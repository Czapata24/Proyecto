using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HankoSpa.Models
{
    public class CrearCitaViewModel
    {
        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        public TimeSpan HoraCita { get; set; }

        [Required]
        public int UsuarioID { get; set; }

        [Required]
        public int Citas_ServiciosID { get; set; }

        public List<SelectListItem> Usuarios { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Servicios { get; set; } = new List<SelectListItem>();
        public ICollection<CitasServicios> CitasServicios { get; internal set; }
    }
}

