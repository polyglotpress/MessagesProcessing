using Microsoft.EntityFrameworkCore;
using ServiceA.Models;

namespace ServiceA.Data
{
    public class ApplicationDbContext : DbContext{

        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { 
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    public DbSet<Message> Messages {get;set;}
    }
}