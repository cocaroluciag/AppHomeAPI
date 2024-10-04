using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAPI.Data;
using HomeAPI.Models;
using HomeAPI.Models.Dto;

namespace HomeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/producto
        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            var productos = _context.Productos.ToList();
            return Ok(productos);
        }

        // GET: api/producto/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // POST: api/producto
        [HttpPost]
        public ActionResult<Producto> CrearProducto([FromBody] CrearProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoProducto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Categoria = dto.Categoria,
                Imagen = dto.Imagen
            };

            _context.Productos.Add(nuevoProducto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.IdProducto }, nuevoProducto);
        }

        // PUT: api/ModificarProducto/{id}
        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] ModificarProductoDto dto)
        {
            var productoExistente = _context.Productos.Find(id);

            if (productoExistente == null)
            {
                return NotFound();
            }

            productoExistente.NombreProducto = dto.NombreProducto;
            productoExistente.Descripcion = dto.Descripcion;
            productoExistente.Precio = dto.Precio;
            productoExistente.Stock = dto.Stock;
            productoExistente.Categoria = dto.Categoria;
            productoExistente.Imagen = dto.Imagen;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/producto/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarProducto(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

