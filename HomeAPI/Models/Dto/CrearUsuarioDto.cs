using System.ComponentModel.DataAnnotations;

public class CrearUsuarioDto // Para POST de Crear Usuarios
{
    [Required]
    [StringLength(50)] // Puedes ajustar la longitud según lo necesario
    public string NombreUsuario { get; set; }

    [Required]
    [EmailAddress] // Validación de correo electrónico
    public string Correo { get; set; }

    [Phone] // Validación de número de teléfono
    public string NumeroTelefonico { get; set; }

    [Required]
    public string NroDocumento { get; set; }

    public string Clave { get; set; }

}

