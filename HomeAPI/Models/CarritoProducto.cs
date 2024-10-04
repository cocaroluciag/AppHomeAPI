using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models
{
    public class CarritoProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Autoincremental
        public int IdCarritoProducto { get; set; }

        [Required]
        public int IdCarrito { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        // Relación con Carrito (muchos a uno)
        [ForeignKey("IdCarrito")]
        public virtual Carrito Carrito { get; set; }

        // Relación con Producto (muchos a uno)
        [ForeignKey("IdProducto")]
        public Producto? Producto { get; set; }
    }
}
