using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeAPI.Data;
using HomeAPI.Models;
using HomeAPI.Models.Dto;

namespace HomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Carrito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            return Ok(await _context.Carritos.ToListAsync());
        }

        // GET: api/Carrito/5
        [HttpGet("{id}")] //Obtener-visualizar Carrito
        public async Task<ActionResult<ObtenerCarritoDto>> GetCarrito(int idCarrito)
        {
            var carrito = await _context.Carritos.FindAsync(idCarrito);

            if (carrito == null)
            {
                return NotFound();
            }

            var obtenerCarritoDto = new ObtenerCarritoDto
            {
                IdCarrito = carrito.IdCarrito,
                IdUsuario = carrito.IdUsuario,
                FechaCreacion = DateTime.Now
                // FechaCreacion = carrito.FechaCreacion   artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();
            };

            return obtenerCarritoDto;
        }

        // PUT: api/Carrito/5  NO ES NECESARIO EL ENDPOINT PUT EN TABLA CARRITO, LOS ATRIBUTOS NO SON MODIFICABLES.
        /*/[HttpPut("{id}")] //Modificar-editar Carrito
        public async Task<IActionResult> PutCarrito(int id, [FromBody] CarritoDto dto)
        {
            if (id != dto.IdCa)
            {
                return BadRequest();
            }

            var carrito = new Carrito
            {
                IdUsuario = dto.IdUsuario,
                FechaCreacion = dto.FechaCreacion
            };

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Carrito
        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito([FromBody] ModificarCarritoDto dto) //Crear Carrito
        {
            var carrito = new Carrito
            {
                IdUsuario = dto.IdUsuario,
                //FechaCreacion = dto.FechaCreacion
            };

            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarrito), new { idCarrito = carrito.IdCarrito }, carrito);

        }

        // DELETE: api/Carrito/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoExists(int id)
        {
            return _context.Carritos.Any(e => e.IdCarrito == id);
        }
    }
}