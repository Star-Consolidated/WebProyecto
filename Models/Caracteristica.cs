using System.Collections.Generic;

namespace WebProyecto.Models
{
    public class Caracteristica
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public ICollection<Opcion> Opciones { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}