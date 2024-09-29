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
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            var usuarioDTOs = usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.idUsuario,
                NombreUsuario = u.NombreUsuario,
                Correo = u.Correo,
                NumeroTelefonico = u.NumeroTelefonico,
                NroDocumento = u.NroDocumento
            }).ToList();

            return usuarioDTOs;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDTO = new UsuarioDto
            {
                IdUsuario = usuario.idUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Correo = usuario.Correo,
                NumeroTelefonico = usuario.NumeroTelefonico,
                NroDocumento = usuario.NroDocumento
            };

            return usuarioDTO;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto usuarioDTO)
        {
            if (id != usuarioDTO.IdUsuario)
            {
                return BadRequest();
            }

            var usuario = new Usuario
            {
                idUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                Correo = usuarioDTO.Correo,
                NumeroTelefonico = usuarioDTO.NumeroTelefonico,
                NroDocumento = usuarioDTO.NroDocumento
                // No asignamos la Clave
            };

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto usuarioDTO)
        {
            var usuario = new Usuario
            {
                NombreUsuario = usuarioDTO.NombreUsuario,
                Correo = usuarioDTO.Correo,
                NumeroTelefonico = usuarioDTO.NumeroTelefonico,
                NroDocumento = usuarioDTO.NroDocumento
                // No asignamos la Clave
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            usuarioDTO.IdUsuario = usuario.idUsuario; // Asignar el Id generado

            return CreatedAtAction("GetUsuario", new { id = usuario.idUsuario }, usuarioDTO);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.idUsuario == id);
        }
    }
}
