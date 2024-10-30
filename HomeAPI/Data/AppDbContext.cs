using Microsoft.EntityFrameworkCore; // Necesario para DbContext y DbSet
using HomeAPI.Models;

namespace HomeAPI.Data
{
    public class AppDbContext : DbContext  // NO MODIFICAR
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }

        //public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoProducto> CarritoProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarritoProducto>()
           .HasKey(cp => cp.IdCarritoProducto); // Configuración de la clave primaria

            // Configurar la relación CarritoProducto -> Carrito
            modelBuilder.Entity<CarritoProducto>()
                .HasOne(cp => cp.Carrito)
                .WithMany(c => c.CarritoProductos)
                .HasForeignKey(cp => cp.IdCarrito)
                .OnDelete(DeleteBehavior.Cascade); // Eliminar en cascada

            // Configurar la relación CarritoProducto -> Producto
            modelBuilder.Entity<CarritoProducto>()
                .HasOne(cp => cp.Producto)
                .WithMany(p => p.CarritoProductos)
                .HasForeignKey(cp => cp.IdProducto)
                .OnDelete(DeleteBehavior.Restrict); // Evitar eliminación si tiene dependencias

            // Configuración de Producto
            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Producto>()
                .Property(p => p.NombreProducto)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Descripcion)
                .HasMaxLength(100);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Stock)
                .IsRequired();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Categoria)
                .HasMaxLength(50);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Imagen)
                .HasMaxLength(100);


            // Configuración de la entidad Usuario

           /* modelBuilder.Entity<Usuario>()

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

                .IsRequired();*/

            //Configuracion de Carrito

            modelBuilder.Entity<Carrito>()
                .HasKey(c => c.IdCarrito); // Establecer IdCarrito como clave primaria

            modelBuilder.Entity<Carrito>()
                .Property(c => c.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");// 

            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Usuario) // Relación 1:1 con Usuario
                .WithOne(u => u.Carrito) // Relación inversa
                .HasForeignKey<Carrito>(c => c.IdUsuario) // Clave foránea en Carrito
                .IsRequired(); // Asegurar que IdUsuario es obligatorio

            // Forzar nombres de tablas en singular

           // modelBuilder.Entity<Usuario>().ToTable("Usuario");

            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Carrito>().ToTable("Carrito");
            modelBuilder.Entity<CarritoProducto>().ToTable("CarritoProducto");
        }
    }
}