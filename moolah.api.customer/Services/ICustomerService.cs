using System.Collections.Generic;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomer(string id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
