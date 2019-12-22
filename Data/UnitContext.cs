using SacramentAgenda.Models;
using Microsoft.EntityFrameworkCore;

namespace SacramentAgenda.Data
{
    public class UnitContext : DbContext
    {
        public UnitContext(DbContextOptions<UnitContext> options) : base(options)
        {
        }

        public DbSet<Agenda> Agendas { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Speaker> Speakers { get; set; }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agenda>().ToTable("Agenda");
            modelBuilder.Entity<Assignment>().ToTable("Assignment");
            modelBuilder.Entity<Speaker>().ToTable("Speaker");
        }
    }
}