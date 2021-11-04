using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ReorderDatabase.Data
{
    internal class NoteContext : DbContext
    {
        private readonly IConfiguration? configuration;
        public NoteContext()
        {
            this.configuration = null;
        }
        public NoteContext(IConfiguration? configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (configuration == null)
            {
                // Only used when generating migrations
                var migrationsConnectionString = @"Server=(localdb)\mssqllocaldb;Database=Notebook;Trusted_Connection=True;ConnectRetryCount=0";
                optionsBuilder.UseSqlServer(migrationsConnectionString, options => options.MigrationsAssembly("ReorderDatabase.Data"));
            }
            else
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Notebook"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Note>()
                .Property(n => n.Order)
                .HasComputedColumnSql("CAST([Numerator] AS DOUBLE PRECISION) / CAST([Denominator] AS DOUBLE PRECISION)", stored: true);
        }

        public DbSet<Entities.Note>? Notes { get; set; }
    }
}