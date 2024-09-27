using System;
using System.Collections.Generic;

namespace HomeAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; } 
        public int Stock { get; set; } 
        public string Categoria { get; set; } 
        public string Imagen { get; set; } 

        // Relación con CarritoProducto
        public virtual ICollection<CarritoProducto> CarritoProductos { get; set; }

    }
}
