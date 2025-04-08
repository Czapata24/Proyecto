using Microsoft.AspNetCore.Mvc;
using DTOs;
using FluentValidation;
using Models;
using Nucleo.Interfaces;

namespace Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaService _citaService;
        private readonly ICitasServiciosService _serviciosService;
        private readonly IValidator<CitaDTO> _validator;

        public CitaController(ICitaService citaService,
                              ICitasServiciosService serviciosService,
                              IValidator<CitaDTO> validator)
        {
            _citaService = citaService;
            _serviciosService = serviciosService;
            _validator = validator;
        }

        // GET: /Cita
        public IActionResult Index()
        {
            var citas = _citaService.ObtenerTodas();
            return View(citas);
        }

        // GET: /Cita/Crear
        public IActionResult Crear()
        {
            ViewBag.Servicios = _serviciosService.ObtenerTodos();
            return View(new CitaDTO());
        }

        // POST: /Cita/Crear
        [HttpPost]
        public IActionResult Crear(CitaDTO citaDto, List<int> ServiciosIds)
        {
            citaDto.ServiciosIds = ServiciosIds;

            var resultado = _validator.Validate(citaDto);
            if (!resultado.IsValid)
            {
                foreach (var error in resultado.Errors)
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                ViewBag.Servicios = _serviciosService.ObtenerTodos();
                return View(citaDto);
            }

            var cita = new Cita
            {
                Paciente = citaDto.Paciente,
                Fecha = citaDto.Fecha,
                Servicios = _serviciosService.ObtenerPorIds(citaDto.ServiciosIds)
            };

            _citaService.Agregar(cita);
            return RedirectToAction("Index");
        }
    }
}
