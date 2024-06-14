using Microsoft.EntityFrameworkCore;
using ApiClient.Models.ApiModels;

namespace WebAPI.Database
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private static string? ConnectionString;
        public DataContext() { }
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DbConnection");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Connect to SQL with connection string from app settings.
            options.UseSqlServer(ConnectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Application> Applications { get; set; }

        // Only for mobile app.
        public DbSet<TokenInfo> TokenInfo { get; set; }
    }
}
