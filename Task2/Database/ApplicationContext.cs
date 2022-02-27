using System.IO;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Task2.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Agent> Agents { get; set; }
        private readonly StreamWriter logStream = new StreamWriter("log.txt", true);
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Client>()
                .HasCheckConstraint("CK_EmailPhoneCheck", "[Email] is not null or [Phone] is not null");
            modelBuilder
                .Entity<Agent>()
                .HasCheckConstraint("CK_DealShareCheck", "[DealShare] between 0 and 100");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.connectionString);
            optionsBuilder.LogTo(logStream.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
        }
        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
    }
}
