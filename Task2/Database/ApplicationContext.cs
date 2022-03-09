using System.IO;
using System.Reflection.Emit;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new AgentConfiguration());
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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder
                .HasCheckConstraint("CK_DealShareCheck", "[DealShare] between 0 and 100");
            builder
                .Property(a => a.DealShare)
                .HasDefaultValue(0)
                .IsRequired(false);
        }
    }
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasCheckConstraint("CK_EmailPhoneCheck", "[Email] is not null or [Phone] is not null");
            builder
                .Property(c => c.LastName)
                .IsRequired(false);
            builder
                .Property(c => c.FirstName)
                .IsRequired(false);
            builder
                .Property(c => c.MiddleName)
                .IsRequired(false);
            builder
                .Property(c => c.Phone)
                .IsRequired(false);
            builder
                .Property(c => c.Email)
                .IsRequired(false);
        }
    }
}
