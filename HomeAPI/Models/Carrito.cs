using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; } // Identificador único del carrito

        [Required]
        public int IdUsuario { get; set; } // Relación 1:1 con Usuario

        [Required]
        public DateTime FechaCreacion { get; set; } // Fecha de creación del carrito

        // Puedes incluir una propiedad de navegación si deseas acceder al Usuario
        public virtual Usuario Usuario { get; set; }

        // Relación muchos a muchos con Producto a través de CarritoProducto
        public ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}