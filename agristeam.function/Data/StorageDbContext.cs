using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace agristeam.function.Data
{
    internal class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options)
        : base(options)
        {
        }
    }
}
