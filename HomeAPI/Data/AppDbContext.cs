using Microsoft.EntityFrameworkCore;
using HomeAPI.Models;

namespace HomeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        //public DbSet<Carrito> Carritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.NombreUsuario)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Correo)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.NumeroTelefonico)
                .HasMaxLength(20);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.NroDocumento)
                .HasMaxLength(20);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Clave)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
