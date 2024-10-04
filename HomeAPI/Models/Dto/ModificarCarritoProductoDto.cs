using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class ModificarCarritoProductoDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }
}
