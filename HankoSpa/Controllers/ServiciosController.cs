namespace HankoSpa.Controllers;

using HankoSpa.Data;
using Microsoft.AspNetCore.Mvc;
using HankoSpa.Models;

public class ServiciosController : Controller
{
    private readonly AppDbContext _context;

    public ServiciosController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var servicios = _context.Servicios.ToList();
        return View(servicios);
    }
}

