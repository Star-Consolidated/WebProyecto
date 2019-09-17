using System;
using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Pedido
    {
        public int ID { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public DateTime Fecha_Entrega { get; set; }
        public bool Estado { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal Precio_Envio { get; set; }
        public ICollection<Detalle_Pedido> Detalle_Pedidos { get; set; }        
    }
}