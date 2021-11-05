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
                .HasComputedColumnSql("CAST([Numerator] AS DECIMAL) / CAST([Denominator] AS DECIMAL)", stored: true);

            modelBuilder.Entity<Entities.Note>()
                .HasData(
                    new Entities.Note { Id = -10, Text = "Ronaldo", Numerator = 1, Denominator = 1 },
                    new Entities.Note { Id = -9, Text = "Roberto Baggio", Numerator = 2, Denominator = 1 },
                    new Entities.Note { Id = -8, Text = "Ferenc Puskás", Numerator = 3, Denominator = 1 },
                    new Entities.Note { Id = -7, Text = "Marco van Basten", Numerator = 4, Denominator = 1 },
                    new Entities.Note { Id = -6, Text = "Stanley Matthews", Numerator = 5, Denominator = 1 },
                    new Entities.Note { Id = -5, Text = "Lev Yashin", Numerator = 6, Denominator = 1 },
                    new Entities.Note { Id = -4, Text = "Alfredo Di Stéfano", Numerator = 7, Denominator = 1 },
                    new Entities.Note { Id = -3, Text = "Diego Maradonna", Numerator = 8, Denominator = 1 },
                    new Entities.Note { Id = -2, Text = "Bobby Moore", Numerator = 9, Denominator = 1 },
                    new Entities.Note { Id = -1, Text = "Pelé", Numerator = 10, Denominator = 1 }
                );
        }

        public DbSet<Entities.Note>? Notes { get; set; }
    }
}