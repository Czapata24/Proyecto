namespace HankoSpa.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; } 

        public List<Cita> Citas { get; set; } 
    }
}
