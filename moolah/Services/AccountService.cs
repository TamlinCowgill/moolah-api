using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Moolah.Api.Controllers;
using Moolah.Api.Domain;
using Moolah.Api.Helpers;

namespace Moolah.Api.Services
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
                if (GetAccount(account.AccountId) != null) throw new ArgumentException(nameof(account.AccountId));
            }

            account.DateCreated = DateTime.Now;
            account.DateUpdated = DateTime.Now;
            account.Balance = 0;

            _dbContext.SaveAsync(account);

            return account;
        }

        public Account UpdateAccount(Account account)
        {
            Validate(account);

            account.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(account);

            Task.WaitAll(task);

            return account;
        }
        private void Validate(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrWhiteSpace(account.Name)) throw new ArgumentNullException(nameof(account.Name));
            if (string.IsNullOrWhiteSpace(account.CustomerId)) throw new ArgumentNullException(nameof(account.CustomerId));
        }
    }
}
