public class CitaDTO
{
    [Required]
    public string NombrePaciente { get; set; }

    [Required]
    public DateTime FechaCita { get; set; }

    [MinLength(1, ErrorMessage = "Debe seleccionar al menos un servicio.")]
    public List<int> ServiciosSeleccionados { get; set; }
}
