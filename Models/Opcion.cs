namespace WebProyecto.Models
{
    public class Opcion
    {
        public int ID { get; set; }
        public int Nombre { get; set; }
        public int CaracteristicaID { get; set; }
        public Caracteristica Caracteristica { get; set; }     
    }
}