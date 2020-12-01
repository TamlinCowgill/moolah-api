using System.Collections.Generic;

namespace Moolah.Customer.Core.Services
{
    public interface ICustomerService
    {
        IEnumerable<Domain.Customer> GetAll();
        Domain.Customer GetCustomer(string id);
        Domain.Customer CreateCustomer(Domain.Customer customer);
        Domain.Customer UpdateCustomer(Domain.Customer customer);
    }
}
