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

        [HttpPost("ValidarCredencial")]
        public async Task<IActionResult> ValidarCredencial([FromBody] UsuarioLoginDto usuario)
        {
            var existeLogin = await _context.Usuarios
                .AnyAsync(x => x.NombreUsuario.Equals(usuario.NombreUsuario) && x.Clave.Equals(usuario.Clave));

            Usuario usuarioLogin = await _context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario.Equals(usuario.NombreUsuario) && x.Clave.Equals(usuario.Clave));


            if (!existeLogin)
            {
                return NotFound("Usuario No Existe");
            }

            LoginResponseDto loginReponse = new LoginResponseDto()
            {
                Autenticado = existeLogin,
                Correo = existeLogin ? usuarioLogin.Correo : "",
                NombreUsuario = existeLogin ? usuarioLogin.NombreUsuario : "",
                idUsuario = existeLogin ? usuarioLogin.idUsuario : 0
            };

            return Ok(loginReponse);
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