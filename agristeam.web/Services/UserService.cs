using agristeam.web.Data;
using agristeam.web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace agristeam.web.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users
                .AsNoTracking()
                .OrderBy(user => user.LastName)
                .ThenBy(user => user.FirstName)
                .ToListAsync();
        }

        public async Task<User?> GetAsync(string userId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.RowKey == userId);
        }

        public async Task CreateAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
