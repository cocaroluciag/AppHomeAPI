using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class ModificarUsuarioDto
    {
        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [EmailAddress] // Validación de correo electrónico
        public string Correo { get; set; }

        [Phone] // Validación de número de teléfono
        public string NumeroTelefonico { get; set; }

        [Required]
        public string NroDocumento { get; set; }

    }
}
