using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Carrito
    {
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public int ProductoID { get; set; }
        public int UsuarioID { get; set; }
        public int ID { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario {get;set;}
        public ICollection<Detalle_Pedido> Detalle_Pedidos { get; set; }
    }
}