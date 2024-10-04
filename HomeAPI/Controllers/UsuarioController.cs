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

        // GET: api/usuario
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return Ok(usuarios);
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")] //Modificar Usuario
        public async Task<IActionResult> PutUsuario(int id, [FromBody] ModificarUsuarioDto dto)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }

            usuarioExistente.NombreUsuario = dto.NombreUsuario ?? usuarioExistente.NombreUsuario;
            usuarioExistente.Correo = dto.Correo ?? usuarioExistente.Correo;
            usuarioExistente.NumeroTelefonico = dto.NumeroTelefonico ?? usuarioExistente.NumeroTelefonico;
            usuarioExistente.NroDocumento = dto.NroDocumento ?? usuarioExistente.NroDocumento;
            //usuarioExistente.Clave = dto.Clave ?? usuarioExistente.Clave;

            _context.Entry(usuarioExistente).State = EntityState.Modified;

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
        public async Task<ActionResult<Usuario>> PostUsuario(CrearUsuarioDto dto) // Crear Usuario 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Correo = dto.Correo,
                NumeroTelefonico = dto.NumeroTelefonico,
                NroDocumento = dto.NroDocumento,
                Clave = dto.Clave
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Usuario.IdUsuario = nuevoUsuario.idUsuario; // Asignar el Id generado
            // return CreatedAtAction("GetUsuario", new { id = nuevoUsuario.idUsuario }, nuevoUsuario);

            return CreatedAtAction(nameof(GetUsuario), new { id = nuevoUsuario.idUsuario }, nuevoUsuario);
        }

        // DELETE: api/Usuarios/{id}
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