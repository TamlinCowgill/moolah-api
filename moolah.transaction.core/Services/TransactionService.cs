using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Moolah.Transaction.Core.Helpers;

namespace Moolah.Transaction.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly ITransactionPublishService _transactionPublishService;

        public TransactionService(IDynamoDBContext dbContext, ITransactionPublishService transactionPublishService)
        {
            _dbContext = dbContext;
            _transactionPublishService = transactionPublishService;
        }

        public IEnumerable<Domain.Transaction> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Domain.Transaction>(new List<ScanCondition>());
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public IEnumerable<Domain.Transaction> GetTransactionForAccount(string accountId)
        {
            var asyncSearch = _dbContext.ScanAsync<Domain.Transaction>(new[] { new ScanCondition("AccountId", ScanOperator.Equal, accountId) });
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public Domain.Transaction GetTransaction(string transactionId)
        {
            var task = _dbContext.LoadAsync<Domain.Transaction>(transactionId);

            Task.WaitAll(task);

            return task.Result;
        }

        public Domain.Transaction CreateTransaction(Domain.Transaction transaction)
        {
            Validate(transaction);

            // If transaction id provided, check there is not an existing transaction with same id
            if (string.IsNullOrWhiteSpace(transaction.TransactionId))
            {
                transaction.TransactionId = DbIdentity.NewId();
            }
            else
            {
                //if (GetTransaction(transaction.TransactionId) != null) throw new AlreadyExistsException("transaction", "transactionid", transaction.TransactionId);
                if (GetTransaction(transaction.TransactionId) != null)
                    throw new Exception("Transaction already exists");
            }

            transaction.DateCreated = DateTime.Now;
            transaction.DateUpdated = DateTime.Now;

            _dbContext.SaveAsync(transaction);

            _transactionPublishService.PublishTransactionCreatedEvent(transaction);

            return transaction;
        }

        public Domain.Transaction UpdateTransaction(Domain.Transaction transaction)
        {
            Validate(transaction);

            transaction.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(transaction);

            Task.WaitAll(task);

            return transaction;
        }

        private void Validate(Domain.Transaction transaction)
        {
            if (transaction == null) throw new Exception();// BadRequestMissingValueException("transaction");
            if (string.IsNullOrWhiteSpace(transaction.AccountId)) throw new Exception();//BadRequestMissingValueException("transaction.accountid");
            if (string.IsNullOrWhiteSpace(transaction.Type)) throw new Exception();//BadRequestMissingValueException("transaction.type");
        }


    }
}
