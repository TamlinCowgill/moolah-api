using System.Collections.Generic;
using System.Threading.Tasks;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> GetAccount(string id);
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(Account account);
    }
}
