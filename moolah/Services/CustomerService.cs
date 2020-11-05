using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Moolah.Api.Domain;
using Moolah.Api.Exceptions;
using Moolah.Api.Helpers;

namespace Moolah.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDynamoDBContext _dbContext;

        public CustomerService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Customer>(new List<ScanCondition>());
            var task = asyncSearch.GetRemainingAsync();
            Task.WaitAll(task);

            return task.Result;
        }

        public Customer GetCustomer(string id)
        {
            var task = _dbContext.LoadAsync<Customer>(id);
            Task.WaitAll(task);

            return task.Result;
        }

        public Customer CreateCustomer(Customer customer)
        {
            Validate(customer);

            if (string.IsNullOrWhiteSpace(customer.CustomerId))
            {
                customer.CustomerId = DbIdentity.NewId();
            }
            else
            {
                if (GetCustomer(customer.CustomerId) != null) throw new AlreadyExistsException(nameof(customer), nameof(customer.CustomerId), customer.CustomerId);
            }

            customer.DateCreated = DateTime.Now;
            customer.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(customer);
            Task.WaitAll(task);

            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            Validate(customer);
            if (GetCustomer(customer.CustomerId) == null) throw new AlreadyExistsException(nameof(customer), nameof(customer.CustomerId), customer.CustomerId);

            customer.DateUpdated = DateTime.Now;

            var task = _dbContext.SaveAsync(customer);

            Task.WaitAll(task);

            return customer;
        }

        private void Validate(Customer customer)
        {
            if (customer == null) throw new BadRequestMissingValueException("customer");
            if (string.IsNullOrWhiteSpace(customer.Name)) throw new BadRequestInvalidValueException("customer.Name");
        }
    }
}
