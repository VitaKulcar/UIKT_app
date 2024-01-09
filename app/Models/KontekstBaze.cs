using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace app.Models
{
	public class KontekstBaze : IdentityDbContext<Vlagatelj>
	{
        public KontekstBaze(DbContextOptions<KontekstBaze> options) : base(options)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Combine(path, "baza.db");
        }

        public string DbPath { get; }

        public KontekstBaze()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            DbPath = System.IO.Path.Combine(path, "baza.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Vlagatelj> Vlagatelj { get; set; }
        public DbSet<Delavec> Delavec { get; set; }
        public DbSet<Vloga> Vloga { get; set; }
    }
}
