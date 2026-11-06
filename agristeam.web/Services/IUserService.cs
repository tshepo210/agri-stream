using System.Collections.Generic;
using System.Threading.Tasks;
using agristeam.web.Models.Entities;

namespace agristeam.web.Services
{
    internal interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetAsync(string userId);
        Task CreateAsync(User user);
    }
}