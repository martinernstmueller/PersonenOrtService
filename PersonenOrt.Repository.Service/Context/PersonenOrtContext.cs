using Microsoft.EntityFrameworkCore;
using PersonenOrt.Framework;

namespace PersonenOrt.Repository.Service.Context
{
    public class PersonenOrtContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Ort> Ort { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PersonOrtDB.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Person>().ToTable("Ort");
        }
    }
}
