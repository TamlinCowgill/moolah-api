using System.Collections.Generic;
using moolah.account.api.Domain;
using moolah.account.api.Models;

namespace moolah.account.api.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();
        Account GetAccount(string id);
        Account CreateAccount(Account account);
        Account UpdateAccount(Account account);
        IEnumerable<Account> GetAccountsForCustomerId(string customerId);
        void AppendTransaction(Transaction transaction);
    }
}
