using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HomeAPI.Models;

namespace HomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static List<Producto> productos = new List<Producto>
        {
            // Agrega más productos aquí
        };

        // GET: api/producto
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public ActionResult<Producto> CreateProducto([FromBody] Producto nuevoProducto)
        {
            nuevoProducto.IdProducto = productos.Max(p => p.IdProducto) + 1; // Asigna un nuevo ID
            productos.Add(nuevoProducto);
            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.IdProducto }, nuevoProducto);
        }

        // PUT: api/producto/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Producto productoActualizado)
        {
            var producto = productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            producto.NombreProducto = productoActualizado.NombreProducto;
            producto.Precio = productoActualizado.Precio;
            return NoContent();
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            productos.Remove(producto);
            return NoContent();
        }
    }
}
