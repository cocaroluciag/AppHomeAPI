using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class LoginResponseDto
    {
        [Key]
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public bool Autenticado { get; set; }
        public string Correo { get; set; }
        public string NumeroTelefonico { get; set; }
        public string NroDocumento { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
