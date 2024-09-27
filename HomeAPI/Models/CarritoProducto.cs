using System;
using System.Collections.Generic;

namespace HomeAPI.Models
{
    public class CarritoProducto
    {
        public int IdCarritoProducto { get; set; } 
        public int IdCarrito { get; set; } 
        public int IdProducto { get; set; } 
        public int Cantidad { get; set; } 

        // Relaciones
        public virtual Carrito Carrito { get; set; } // Llave foránea a Carrito
        public virtual Producto Producto { get; set; } // Llave foránea a Producto
    }
}
