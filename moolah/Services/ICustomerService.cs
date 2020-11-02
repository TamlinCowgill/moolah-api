using System.Collections.Generic;
using System.Threading.Tasks;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetCustomer(string id);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
    }
}
