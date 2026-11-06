using agristeam.web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace agristeam.web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
