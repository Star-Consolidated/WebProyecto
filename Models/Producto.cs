using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public float Peso { get; set; }
        public string Descripcion { get; set; }
        public int Tienda_ID { get; set; }
        public int Caracteristica_ID { get; set; }
        public Tienda Tienda { get; set; }
        public Caracteristica Caracteristica { get; set; }
        public ICollection<Carrito> Carritos { get; set; }
        private ICollection<Producto_Categoria> Producto_Categorias{get;set;}
    }
}