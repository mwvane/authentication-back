using Microsoft.EntityFrameworkCore;

namespace authentication_back.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<User> Users { get; set; } 
    }
}
