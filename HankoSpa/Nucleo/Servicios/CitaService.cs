<<<<<<< Updated upstream
public class CitaService : ICitaService
{
    private readonly List<Cita> _citas = new();
    private int _idActual = 1;

    public void AgregarCita(Cita cita)
    {
        cita.Id = _idActual++;
        _citas.Add(cita);
    }

    public List<Cita> ObtenerCitas() => _citas;

    public Cita? ObtenerCitaPorId(int id) => _citas.FirstOrDefault(c => c.Id == id);
=======
using HankoSpa.Models;
using HankoSpa.DTOs;

public class CitaService : ICitaService
{
    private readonly List<Cita> _citas = new();
    private readonly List<Citas_Servicios> _servicios = new()
    {
        new Citas_Servicios { Id = 1, Nombre = "Masaje Relajante", Precio = 100 },
        new Citas_Servicios { Id = 2, Nombre = "Limpieza Facial", Precio = 80 },
        new Citas_Servicios { Id = 3, Nombre = "Depilación", Precio = 50 }
    };

    public List<Cita> ObtenerTodas()
    {
        return _citas;
    }

    public void Crear(CitaDTO citaDto)
    {
        var nuevaCita = new Cita
        {
            Id = _citas.Count + 1,
            NombrePaciente = citaDto.NombrePaciente,
            Fecha = citaDto.Fecha,
            Servicios = _servicios
                .Where(s => citaDto.ServiciosSeleccionados.Contains(s.Id))
                .ToList()
        };

        _citas.Add(nuevaCita);
    }

    public List<Citas_Servicios> ObtenerServiciosDisponibles()
    {
        return _servicios;
    }
>>>>>>> Stashed changes
}
