using System.Collections.Generic;
using System.Threading.Tasks;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
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
