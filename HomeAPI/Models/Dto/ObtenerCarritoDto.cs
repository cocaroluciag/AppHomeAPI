using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class ObtenerCarritoDto
    {
        [Key] 
        public int IdCarrito { get; set; } // Identificador único del carrito

        [Required]
        public int IdUsuario { get; set; } // Relación 1:1 con Usuario

        [Required]
        public DateTime FechaCreacion { get; set; } // Fecha de creación del carrito
    }
}