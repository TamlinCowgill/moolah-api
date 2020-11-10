using System.Collections.Generic;
using moolah.api.customer.Domain;

namespace moolah.api.customer.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomer(string id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
    }
}
