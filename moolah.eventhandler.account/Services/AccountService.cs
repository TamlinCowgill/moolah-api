using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using moolah.api.account.Domain;
using moolah.api.account.Exceptions;
using moolah.api.account.Helpers;
using moolah.api.account.Models;

namespace moolah.api.account.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDynamoDBContext _dbContext;

        public AccountService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Account> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Account>(new List<ScanCondition>());
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public IEnumerable<Account> GetAccountsForCustomerId(string customerId)
        {
            var asyncSearch = _dbContext.ScanAsync<Account>(new[] { new ScanCondition("CustomerId", ScanOperator.Equal, customerId) });
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public void AppendTransaction(Transaction transaction)
        {
            Validate(transaction);

            var account = GetAccount(transaction.AccountId);
            if (account == null) throw new ArgumentNullException("Unknown account id: " + transaction.AccountId);

            account.TransactionSummary ??= new List<TransactionRunTotal>();

            if (account.TransactionSummary.Any(t => t.Id.Equals(transaction.TransactionId)))
            {
                LambdaLogger.Log($"Transaction with id : {transaction.TransactionId} already found in account summary");
                return;
            }

            account.TransactionSummary.Add(new TransactionRunTotal
            {
                Id = transaction.TransactionId,
                Date = transaction.Date,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Type = transaction.Type,
                RunningTotal = 0
            });

            account.TransactionSummary = account.TransactionSummary.OrderBy(o => o.Date).ThenBy(o => o.Id).ToList();

            var runningTotal = 0m;
            account.TransactionSummary.ForEach(t =>
            {
                runningTotal += t.Amount;
                t.RunningTotal = runningTotal;
            });

            account.Balance = runningTotal;
            account.DateUpdated = DateTime.Now;

            UpdateAccount(account);
        }

        public Account GetAccount(string accountId)
        {
            var task = _dbContext.LoadAsync<Account>(accountId);

            Task.WaitAll(task);

            return task.Result;
        }

        public Account CreateAccount(Account account)
        {
            Validate(account);

            // If account id provided, check there is not an existing account with same id
            if (string.IsNullOrWhiteSpace(account.AccountId))
            {
                account.AccountId = DbIdentity.NewId();
            }
            else
            {
                if (GetAccount(account.AccountId) != null) throw new AlreadyExistsException("account", "accountid", account.AccountId);
            }

            account.DateCreated = DateTime.Now;
            account.DateUpdated = DateTime.Now;
            account.Balance = 0;

            _dbContext.SaveAsync(account).Wait();

            return account;
        }

        public Account UpdateAccount(Account account)
        {
            Validate(account);

            account.DateUpdated = DateTime.Now;

            _dbContext.SaveAsync(account).Wait();

            return account;
        }

        private void Validate(Account account)
        {
            if (account == null) throw new BadRequestMissingValueException("account");
            if (string.IsNullOrWhiteSpace(account.Name)) throw new BadRequestMissingValueException("account.name");
            if (string.IsNullOrWhiteSpace(account.CustomerId)) throw new BadRequestMissingValueException("account.customerid");
        }

        private void Validate(Transaction transaction)
        {
            if (transaction == null) throw new BadRequestMissingValueException("transaction");
            if (string.IsNullOrWhiteSpace(transaction.TransactionId)) throw new BadRequestMissingValueException("transaction.TransactionId");
            if (string.IsNullOrWhiteSpace(transaction.AccountId)) throw new BadRequestMissingValueException("transaction.AccountId");
            if (string.IsNullOrWhiteSpace(transaction.Type)) throw new BadRequestMissingValueException("transaction.Type");
        }
    }
}
