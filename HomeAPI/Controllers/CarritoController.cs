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
        public async Task<ActionResult<IEnumerable<CarritoDto>>> GetCarritos()
        {
            var carritos = await _context.Carritos.ToListAsync();
            var carritoDTOs = carritos.Select(c => new CarritoDto
            {
                IdCarrito = c.IdCarrito,
                IdUsuario = c.IdUsuario,
                FechaCreacion = c.FechaCreacion
            }).ToList();

            return carritoDTOs;
        }

        // GET: api/Carrito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            var carritoDTO = new CarritoDto
            {
                IdCarrito = carrito.IdCarrito,
                IdUsuario = carrito.IdUsuario,
                FechaCreacion = carrito.FechaCreacion
            };

            return carritoDTO;
        }

        // PUT: api/Carrito/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, CarritoDto carritoDTO)
        {
            if (id != carritoDTO.IdCarrito)
            {
                return BadRequest();
            }

            var carrito = new Carrito
            {
                IdCarrito = carritoDTO.IdCarrito,
                IdUsuario = carritoDTO.IdUsuario,
                FechaCreacion = carritoDTO.FechaCreacion
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
        }

        // POST: api/Carrito
        [HttpPost]
        public async Task<ActionResult<CarritoDto>> PostCarrito(CarritoDto carritoDTO)
        {
            var carrito = new Carrito
            {
                IdUsuario = carritoDTO.IdUsuario,
                FechaCreacion = carritoDTO.FechaCreacion
            };

            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            carritoDTO.IdCarrito = carrito.IdCarrito; // Asignar el Id generado

            return CreatedAtAction("GetCarrito", new { id = carrito.IdCarrito }, carritoDTO);
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
