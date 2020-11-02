using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Moolah.Api.Domain;

namespace Moolah.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDynamoDBContext _dbContext;

        public CustomerService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<Customer>(new List<ScanCondition>());
            var task = await asyncSearch.GetRemainingAsync();

            return Task.FromResult(task).Result;
        }

        public async Task<Customer> GetCustomer(string id)
        {
            var task = await _dbContext.LoadAsync<Customer>(id);

            return Task.FromResult(task).Result;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            Validate(customer);

            customer.Id = Guid.NewGuid().ToString();
            customer.DateCreated = DateTime.Now;
            customer.DateUpdated = DateTime.Now;

            await _dbContext.SaveAsync(customer);

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            Validate(customer);
            
            customer.DateUpdated = DateTime.Now;

            await _dbContext.SaveAsync(customer);

            return customer;
        }

        private void Validate(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (string.IsNullOrWhiteSpace(customer.Name)) throw new ArgumentNullException(nameof(customer.Name));
        }
    }
}
