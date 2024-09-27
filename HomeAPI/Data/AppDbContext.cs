using Microsoft.EntityFrameworkCore; // Necesario para DbContext y DbSet
using HomeAPI.Models;

namespace HomeAPI.Data
{
    public class AppDbContext : DbContext  // NO MODIFICAR
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
