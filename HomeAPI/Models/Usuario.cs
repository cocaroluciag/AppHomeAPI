using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models;

public class Usuario
{
    [Key]
    public int idUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Correo { get; set; }
    public string NumeroTelefonico { get; set; }
    public string NroDocumento { get; set; }
    public string Clave { get; set; }

    public Carrito Carrito { get; set; }
}
