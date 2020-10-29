using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using moolah.Domain;

namespace moolah.Services
{
    public class UserService : IUserService
    {
        private readonly IDynamoDBContext _dbContext;

        public UserService(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var asyncSearch = _dbContext.ScanAsync<User>(new List<ScanCondition>());
            var task = await asyncSearch.GetRemainingAsync();

            return Task.FromResult(task).Result;
        }

        public async Task<User> GetUser(string id)
        {
            var task = await _dbContext.LoadAsync<User>(id);

            return Task.FromResult(task).Result;
        }

        public async Task<string> CreateUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var newUser = new User { Name = name, DateCreated = DateTime.Now };
            await _dbContext.SaveAsync(newUser);

            return newUser.Id;
        }

        public void UpdateUser(User user)
        {
            user.DateUpdated = DateTime.Now;
            _dbContext.SaveAsync(user);
        }
    }
}
