using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class UsuarioLoginDto
    {
        public string NombreUsuario { get; set; }


        [Required]
        public string Clave { get; set; }
    }
}
