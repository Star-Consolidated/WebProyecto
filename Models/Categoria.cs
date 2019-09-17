using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public ICollection<Producto_Categoria> Producto_Categorias { get; set; }
    }
}