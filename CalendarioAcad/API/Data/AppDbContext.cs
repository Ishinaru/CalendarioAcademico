using Microsoft.EntityFrameworkCore;
using API.Model;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Calendario>()
                .HasMany(e => e.Eventos)
                .WithOne(e => e.Calendario)
                .HasForeignKey(e => e.IdCalendario)
                .IsRequired();

            modelBuilder.Entity<Calendario>()
                .HasMany(e => e.Historicos)
                .WithOne(e => e.Calendario)
                .HasForeignKey(e => e.IdCalendario)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Historicos)
                .WithOne(e => e.Evento)
                .HasForeignKey(e => e.IdEvento)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Eventos_Portarias)
                .WithOne(e => e.Evento)
                .HasForeignKey(e => e.IdEvento);

            modelBuilder.Entity<Portaria>()
                .HasMany(e => e.Historicos)
                .WithOne(e => e.Portaria)
                .HasForeignKey(e => e.IdPortaria)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Portaria>()
                .HasMany(e => e.Eventos_Portarias)
                .WithOne(e => e.Portaria)
                .HasForeignKey(e => e.IdPortaria);

        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Portaria> Portarias { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Evento_Portaria> Evento_Portarias { get; set; }
    }
}
