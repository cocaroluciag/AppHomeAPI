using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreProducto { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }

        [StringLength(500)]
        public string Imagen { get; set; }

        // Relación con CarritoProducto (uno a muchos)
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }
    }
}