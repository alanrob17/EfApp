using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfApp.Models
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public AppDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RecordDbConnection");
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}