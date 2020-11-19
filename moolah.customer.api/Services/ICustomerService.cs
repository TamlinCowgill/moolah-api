using System.Collections.Generic;
using moolah.customer.api.Domain;

namespace moolah.customer.api.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomer(string id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
