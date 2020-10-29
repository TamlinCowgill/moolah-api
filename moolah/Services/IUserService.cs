using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moolah.Domain;

namespace moolah.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(string id);
        Task<string> CreateUser(string name);
        void UpdateUser(User user);
    }
}
