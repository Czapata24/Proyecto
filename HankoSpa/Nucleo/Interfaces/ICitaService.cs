<<<<<<< Updated upstream
public interface ICitaService
{
    void AgregarCita(Cita cita);
    List<Cita> ObtenerCitas();
    Cita? ObtenerCitaPorId(int id);
=======
using HankoSpa.DTOs;
using HankoSpa.Models;

public interface ICitaService
{
    List<Cita> ObtenerTodas();
    void Crear(CitaDTO citaDto);
    List<Citas_Servicios> ObtenerServiciosDisponibles();
>>>>>>> Stashed changes
}
