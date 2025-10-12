using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConfig = App.Configuration.GetSection("ConnectionStrings");
            optionsBuilder.UseNpgsql(dbConfig["db"]);
        }
    }
}
