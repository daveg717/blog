using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DbLogic : DbContext
    {
        public DbLogic(DbContextOptions<DbLogic> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Post> Posts => Set<Post>();
    }
}
