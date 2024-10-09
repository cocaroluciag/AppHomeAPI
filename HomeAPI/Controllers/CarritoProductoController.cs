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
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoProductoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritoProductoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/carritoproducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoProducto>>> GetCarritoProductos()
        {
            return Ok(await _context.CarritoProductos.ToListAsync());
        }

        // GET: api/carritoproducto/{idCarrito}
        [HttpGet("{idCarrito}")]
        public ActionResult<IEnumerable<CarritoProductoDto>> ObtenerCarritoProducto(int idCarrito)
        {
            var carritoProductos = _context.CarritoProductos
                .Where(cp => cp.IdCarrito == idCarrito)
                .Select(cp => new CarritoProductoDto
                {
                    IdProducto = cp.IdProducto,
                    Cantidad = cp.Cantidad,
                    NombreProducto = cp.Producto.NombreProducto,
                    Precio = cp.Producto.Precio,
                    Imagen = cp.Producto.Imagen
                })
                .ToList();

            if (!carritoProductos.Any())
            {
                return NotFound("No se encontraron productos en el carrito.");
            }

            return Ok(carritoProductos);
        }

        // POST: api/carritoproducto
        [HttpPost]
        public ActionResult<CarritoProducto> CreateCarritoProducto([FromBody] CrearCarritoProductoDto dto)
        {
            // Validar si el carrito existe antes de agregar productos
            var carrito = _context.Carritos.Find(dto.IdCarrito);
            if (carrito == null)
            {
                return NotFound("CARRITO NO ENCONTRADO.");
            }

            // Validar si el producto existe
            var producto = _context.Productos.Find(dto.IdProducto);
            if (producto == null)
            {
                return NotFound("PRODUCTO NO ENCONTRADO.");
            }

            // Validar que la cantidad solicitada no exceda el stock disponible
            if (dto.Cantidad > producto.Stock)
            {
                return BadRequest($"LA CANTIDAD SOLICITADA DE DICHO PRODUCTO({dto.Cantidad}) EXCEDE EL STOCK DISPONIBLE ({producto.Stock}).");
            }

            // Crear el CarritoProducto
            var carritoProducto = new CarritoProducto
            {
                IdCarrito = dto.IdCarrito,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad
            };

            _context.CarritoProductos.Add(carritoProducto);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObtenerCarritoProducto), new { idCarrito = carritoProducto.IdCarrito }, carritoProducto);
        }

        // PUT: api/carritoproducto/{id} 
        [HttpPut("{id}")]
        public IActionResult ModificarCarritoProducto(int id, [FromBody] ModificarCarritoProductoDto dto)
        {
            var carritoProducto = _context.CarritoProductos.Find(id);

            if (carritoProducto == null)
            {
                return NotFound("PRODUCTO NO ENCONTRADO EN EL CARRITO");
            }

            // Validar si el producto existe
            var producto = _context.Productos.Find(carritoProducto.IdProducto);
            if (producto == null)
            {
                return NotFound("PRODUCTO NO ENCONTRADO");
            }

            // Validar que la cantidad solicitada no exceda el stock disponible
            if (dto.Cantidad > producto.Stock)
            {
                return BadRequest($"LA CANTIDAD SOLICITADA ({dto.Cantidad}) EXCEDE EL STOCK DISPONIBLE ({producto.Stock}).");
            }

            // Actualizar la cantidad del producto en el carrito
            carritoProducto.Cantidad = dto.Cantidad;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/carritoproducto/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoProducto(int id)
        {
            var carritoProducto = await _context.CarritoProductos.FindAsync(id);

            if (carritoProducto == null)
            {
                return NotFound();
            }

            _context.CarritoProductos.Remove(carritoProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoProductoExists(int id)
        {
            return _context.CarritoProductos.Any(e => e.IdCarritoProducto == id);
        }
    }
}