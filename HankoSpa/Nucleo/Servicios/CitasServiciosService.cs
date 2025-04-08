using Models;
using Nucleo.Interfaces;

namespace Nucleo.Servicios
{
    public class CitasServiciosService : ICitasServiciosService
    {
        private readonly List<Citas_Servicios> _servicios;

        public CitasServiciosService()
        {
            // Simulación de servicios cargados en memoria
            _servicios = new List<Citas_Servicios>
            {
                new Citas_Servicios { Id = 1, Nombre = "Masaje Relajante", Precio = 100.00m },
                new Citas_Servicios { Id = 2, Nombre = "Limpieza Facial", Precio = 80.00m },
                new Citas_Servicios { Id = 3, Nombre = "Manicure", Precio = 50.00m },
                new Citas_Servicios { Id = 4, Nombre = "Pedicure", Precio = 60.00m }
            };
        }

        public List<Citas_Servicios> ObtenerTodos()
        {
            return _servicios;
        }

        public Citas_Servicios? ObtenerPorId(int id)
        {
            return _servicios.FirstOrDefault(s => s.Id == id);
        }

        public List<Citas_Servicios> ObtenerPorIds(IEnumerable<int> ids)
        {
            return _servicios.Where(s => ids.Contains(s.Id)).ToList();
        }
    }
}
