using System.Collections.Generic;
namespace WebProyecto.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public int Telefono { get; set; }
        public string Direcction { get; set; }
        public ICollection<Carrito> Carritos { get; set; }
    }
}