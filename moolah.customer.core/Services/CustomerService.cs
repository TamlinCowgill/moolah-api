using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Moolah.Customer.Core.Helpers;

namespace Moolah.Customer.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDynamoDBContext _dbContext;

        public CustomerService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Domain.Customer> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Domain.Customer>(new List<ScanCondition>());
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public Domain.Customer GetCustomer(string id)
        {
            var task = _dbContext.LoadAsync<Domain.Customer>(id);
            Task.WaitAll(task);

            return task.Result;
        }

        public Domain.Customer CreateCustomer(Domain.Customer customer)
        {
            Validate(customer);

            if (string.IsNullOrWhiteSpace(customer.CustomerId))
            {
                customer.CustomerId = DbIdentity.NewId();
            }
            else
            {
                if (GetCustomer(customer.CustomerId) != null) throw new Exception("Customer already exists");
                //if (GetCustomer(customer.CustomerId) != null) throw new AlreadyExistsException(nameof(customer), nameof(customer.CustomerId), customer.CustomerId);
            }

            customer.DateCreated = DateTime.Now;
            customer.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(customer);
            Task.WaitAll(task);

            return customer;
        }

        public Domain.Customer UpdateCustomer(Domain.Customer customer)
        {
            Validate(customer);
            //if (GetCustomer(customer.CustomerId) == null) throw new AlreadyExistsException(nameof(customer), nameof(customer.CustomerId), customer.CustomerId);
            if (GetCustomer(customer.CustomerId) == null) throw new Exception("Customer already exists");

            customer.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(customer);

            Task.WaitAll(task);

            return customer;
        }

        private void Validate(Domain.Customer customer)
        {
            //           if (customer == null) throw new BadRequestMissingValueException("customer");
            //           if (string.IsNullOrWhiteSpace(customer.Name)) throw new BadRequestInvalidValueException("customer.Name");
            if (customer == null) throw new Exception("customer name not valid");
            if (string.IsNullOrWhiteSpace(customer.Name)) throw new Exception("customer.Name not valid");
        }
    }
}
