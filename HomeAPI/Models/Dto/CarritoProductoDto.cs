namespace HomeAPI.Models.Dto
{
    public class CarritoProductoDto // para mostrar datos en el Get
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
    }
}
