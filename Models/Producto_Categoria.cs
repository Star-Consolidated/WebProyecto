namespace WebProyecto.Models
{
    public class Producto_Categoria
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public int CategoriaID { get; set; }
        public Producto Producto { get; set; }
        public Categoria Categoria { get; set; }   
    }
}