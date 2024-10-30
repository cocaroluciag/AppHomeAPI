using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class ModificarCarritoDto //para Editar 
    {
        [Required]
        public int IdUsuario { get; set; } // Relación 1:1 con Usuario

        [Required]
        public DateTime FechaCreacion { get; set; } // Fecha de creación del carrito
    }
}
