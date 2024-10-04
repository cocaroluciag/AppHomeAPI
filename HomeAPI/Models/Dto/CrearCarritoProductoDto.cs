using System.ComponentModel.DataAnnotations;

namespace HomeAPI.Models.Dto
{
    public class CrearCarritoProductoDto
    {
       // [Required]
        public int IdCarrito { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }
}
