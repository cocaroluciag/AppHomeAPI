using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HomeAPI.Models;

namespace HomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoProductoController : ControllerBase
    {
        private static List<CarritoProducto> carritoProductos = new List<CarritoProducto>
        {
            new CarritoProducto { IdCarritoProducto = 1, IdProducto = 1, Cantidad = 2 },
            new CarritoProducto { IdCarritoProducto = 2, IdProducto = 2, Cantidad = 1 }
            // Agrega más productos al carrito aquí
        };

        // GET: api/carritoproducto
        [HttpGet]
        public ActionResult<IEnumerable<CarritoProducto>> GetCarritoProductos()
        {
            return Ok(carritoProductos);
        }

        // GET: api/carritoproducto/{id}
        [HttpGet("{id}")]
        public ActionResult<CarritoProducto> GetCarritoProducto(int id)
        {
            var carritoProducto = carritoProductos.FirstOrDefault(cp => cp.IdProducto == id);
            if (carritoProducto == null)
            {
                return NotFound();
            }
            return Ok(carritoProducto);
        }

        // POST: api/carritoproducto
        [HttpPost]
        public ActionResult<CarritoProducto> CreateCarritoProducto([FromBody] CarritoProducto nuevoCarritoProducto)
        {
            nuevoCarritoProducto.IdProducto = carritoProductos.Max(cp => cp.IdProducto) + 1; // Asigna un nuevo ID
            carritoProductos.Add(nuevoCarritoProducto);
            return CreatedAtAction(nameof(GetCarritoProducto), new { id = nuevoCarritoProducto.IdProducto }, nuevoCarritoProducto);
        }

        // PUT: api/carritoproducto/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCarritoProducto(int id, [FromBody] CarritoProducto carritoProductoActualizado)
        {
            var carritoProducto = carritoProductos.FirstOrDefault(cp => cp.IdProducto == id);
            if (carritoProducto == null)
            {
                return NotFound();
            }
            carritoProducto.IdProducto = carritoProductoActualizado.IdProducto;
            carritoProducto.Cantidad = carritoProductoActualizado.Cantidad;
            return NoContent();
        }

        // DELETE: api/carritoproducto/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCarritoProducto(int id)
        {
            var carritoProducto = carritoProductos.FirstOrDefault(cp => cp.IdCarritoProducto == id);
            if (carritoProducto == null)
            {
                return NotFound();
            }
            carritoProductos.Remove(carritoProducto);
            return NoContent();
        }
    }
}
