namespace HomeAPI.Models.Dto
{
    public class CrearProductoDto
    {
        public string NombreProducto { get; set; }  
        public string Descripcion { get; set; }        
        public decimal Precio { get; set; }            
        public int Stock { get; set; }                  
        public string Categoria { get; set; }          
        public string Imagen { get; set; }
    }
}
