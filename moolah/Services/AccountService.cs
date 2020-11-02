using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDynamoDBContext _dbContext;

        public AccountService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Account>(new List<ScanCondition>());
            var task = await asyncSearch.GetRemainingAsync();

            return Task.FromResult(task).Result;
        }

        public async Task<Account> GetAccount(string id)
        {
            var task = await _dbContext.LoadAsync<Account>(id);

            return Task.FromResult(task).Result;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            Validate(account);

            account.Id = Guid.NewGuid().ToString();
            account.DateCreated = DateTime.Now;
            account.DateUpdated = DateTime.Now;
            account.DateOpened = DateTime.Now;
            account.Balance = 0;

            await _dbContext.SaveAsync(account);

            return account;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            Validate(account);

            account.DateUpdated = DateTime.Now;

            await _dbContext.SaveAsync(account);

            return account;
        }
        private void Validate(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrWhiteSpace(account.Name)) throw new ArgumentNullException(nameof(account.Name));
            if (string.IsNullOrWhiteSpace(account.UserId)) throw new ArgumentNullException(nameof(account.UserId));
        }
    }
}
