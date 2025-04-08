using Microsoft.AspNetCore.Mvc;
using Nucleo.Interfaces;
using Models;

namespace Controllers
{
    public class CitasServiciosController : Controller
    {
        private readonly ICitasServiciosService _serviciosService;

        public CitasServiciosController(ICitasServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }

        public IActionResult Index()
        {
            var servicios = _serviciosService.ObtenerTodos();
            return View(servicios);
        }
    }
}

