public interface ICitaService
{
    void AgregarCita(Cita cita);
    List<Cita> ObtenerCitas();
    Cita? ObtenerCitaPorId(int id);
}
