public class CitaResponseDTO
{
    public int Id { get; set; }
    public string NombrePaciente { get; set; }
    public DateTime Fecha { get; set; }
    public List<string> Servicios { get; set; } = new();
}
