using System.Collections.Generic;
using moolah.api.account.Domain;
using moolah.api.account.Models;

namespace moolah.api.account.Services
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
