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
}
