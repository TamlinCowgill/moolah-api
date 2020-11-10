using System.Collections.Generic;
using moolah.api.account.Domain;

namespace moolah.api.account.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();
        Account GetAccount(string id);
        Account CreateAccount(Account account);
        Account UpdateAccount(Account account);
        IEnumerable<Account> GetAccountsForCustomerId(string customerId);
    }
}
