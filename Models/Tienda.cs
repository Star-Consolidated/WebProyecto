using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Tienda
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int Telefono { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}