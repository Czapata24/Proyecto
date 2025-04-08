using HankoSpa.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HankoSpa.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HankoSpa.Controllers
{
    public class CitasController : Controller
    {
        private readonly AppDbContext _context;

        public CitasController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Crear()
        {
            var model = new CrearCitaViewModel
            {
                Usuarios = _context.UsuarioID.Select(static u => new SelectListItem
                {
                    Value = u.UsuarioID.ToString(),
                    Text = u.Nombre
                }).ToList(),

                Servicios = _context.Servicios.Select(s => new SelectListItem
                {
                    Value = s.ServicioId.ToString(), 
                    Text = s.Nombre
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearCitaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cita = new Cita
                {
                    FechaCita = model.FechaCita,
                    HoraCita = model.HoraCita,
                    UsuarioID = model.UsuarioID,
                    CitasServicios = model.CitasServicios,
                    EstadoCita = "Asignada"
                };

                _context.Citas.Add(cita);
                await _context.SaveChangesAsync();

                return RedirectToAction("Confirmacion");
            }

            return View(model);
        }

        public async Task<IActionResult> Cancelar(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            cita.EstadoCita = "Cancelada";
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Confirmacion()
        {
            return View();
        }
    }
}