namespace WebProyecto.Models
{
    public class Detalle_Pedido
    {
        public int ID {get;set;}
        public int CarritoProductoID { get; set; }
        public int CarritoUsuarioID { get; set; }
        public int CarritoID { get; set; }
        public int PedidoID { get; set; }
        public Carrito Carrito { get; set; }
        public Pedido Pedido { get; set; }
    }
}