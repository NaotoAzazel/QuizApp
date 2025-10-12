using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: add loading the config file
            optionsBuilder.UseNpgsql("Host=localhost;Port=5555;Username=username;Password=pass;Database=db;");
        }
    }
}
