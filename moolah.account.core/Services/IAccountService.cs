using System.Collections.Generic;
using Moolah.Account.Core.Models;

namespace Moolah.Account.Core.Services
{
    public interface IAccountService
    {
        IEnumerable<Domain.Account> GetAll();
        Domain.Account GetAccount(string id);
        Domain.Account CreateAccount(Domain.Account account);
        Domain.Account UpdateAccount(Domain.Account account);
        IEnumerable<Domain.Account> GetAccountsForCustomerId(string customerId);
        void AppendTransaction(Transaction transaction);
    }
}
