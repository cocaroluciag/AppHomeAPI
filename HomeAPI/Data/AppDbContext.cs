using Microsoft.EntityFrameworkCore;
using HomeAPI.Models;

namespace HomeAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } // Corrección: Usamos el plural por convención
        public DbSet<Carrito> Carritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.idUsuario);

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

            //Configuracion de Carrito

            modelBuilder.Entity<Carrito>()
                .HasKey(c => c.IdCarrito); // Establecer IdCarrito como clave primaria

            modelBuilder.Entity<Carrito>()
                .Property(c => c.FechaCreacion)
                .IsRequired(); // Asegurar que FechaCreacion es obligatorio

            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Usuario) // Relación 1:1 con Usuario
                .WithOne(u => u.Carrito) // Relación inversa
                .HasForeignKey<Carrito>(c => c.IdUsuario) // Clave foránea en Carrito
                .IsRequired(); // Asegurar que IdUsuario es obligatorio
        }
    }
}
