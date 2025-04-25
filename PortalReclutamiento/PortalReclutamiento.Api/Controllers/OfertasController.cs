using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalReclutamiento.PortalReclutamiento.Api.Controllers
{
    public class OfertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string ubicacion = "", string categoria = "", string ordenamiento = "")
        {
            try
            {
                if (!_context.Ofertas.Any())
                {
                    ViewBag.Ubicaciones = new List<string>();
                    ViewBag.Categorias = new List<string>();
                    ViewBag.Mensaje = "No hay ofertas disponibles en este momento.";
                    return View(new List<Oferta>());
                }

                var ofertas = _context.Ofertas.AsQueryable();

                ViewBag.Ubicaciones = await _context.Ofertas.Select(o => o.Ubicacion).Distinct().ToListAsync();
                ViewBag.Categorias = await _context.Ofertas.Select(o => o.Categoria).Distinct().ToListAsync();

                if (!string.IsNullOrEmpty(ubicacion))
                {
                    ofertas = ofertas.Where(o => o.Ubicacion == ubicacion);
                    ViewBag.UbicacionSeleccionada = ubicacion;
                }

                if (!string.IsNullOrEmpty(categoria))
                {
                    ofertas = ofertas.Where(o => o.Categoria == categoria);
                    ViewBag.CategoriaSeleccionada = categoria;
                }

                switch (ordenamiento)
                {
                    case "salario_asc":
                        ofertas = ofertas.OrderBy(o => o.Salario);
                        break;
                    case "salario_desc":
                        ofertas = ofertas.OrderByDescending(o => o.Salario);
                        break;
                    case "fecha_asc":
                        ofertas = ofertas.OrderBy(o => o.FechaPublicacion);
                        break;
                    case "fecha_desc":
                        ofertas = ofertas.OrderByDescending(o => o.FechaPublicacion);
                        break;
                    default:
                        ofertas = ofertas.OrderByDescending(o => o.FechaPublicacion);
                        break;
                }

                ViewBag.Ordenamiento = ordenamiento;

                return View(await ofertas.ToListAsync());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar las ofertas. Por favor, inténtelo de nuevo más tarde.";
                ViewBag.Ubicaciones = new List<string>();
                ViewBag.Categorias = new List<string>();
                return View(new List<Oferta>());
            }
        }

        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var oferta = await _context.Ofertas
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (oferta == null)
                {
                    return NotFound();
                }

                return View(oferta);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Empresa,Ubicacion,Salario,Categoria,Descripcion")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    oferta.FechaPublicacion = DateTime.Now;
                    _context.Add(oferta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Oferta creada correctamente.";
                    return RedirectToAction(nameof(Index), "Admin", new { area = "" });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear la oferta. Por favor, intenta nuevamente.");
                }
            }
            return View(oferta);
        }

       
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }
            return View(oferta);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Titulo,Empresa,Ubicacion,Salario,Categoria,Descripcion,FechaPublicacion")] Oferta oferta)
        {
            if (id != oferta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oferta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Oferta actualizada correctamente.";
                    return RedirectToAction("Ofertas", "Admin");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaExists(oferta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar la oferta. Por favor, intenta nuevamente.");
                }
            }
            return View(oferta);
        }

        
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oferta == null)
            {
                return NotFound();
            }

            return View(oferta);
        }

       
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                var oferta = await _context.Ofertas.FindAsync(id);
                if (oferta != null)
                {
                    _context.Ofertas.Remove(oferta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Oferta eliminada correctamente.";
                }
                return RedirectToAction("Ofertas", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar la oferta. Es posible que tenga aplicaciones asociadas.";
                return RedirectToAction("Ofertas", "Admin");
            }
        }

        private bool OfertaExists(int id)
        {
            return _context.Ofertas.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aplicar(int id, [Bind("Nombre,Email,Telefono")] Aplicacion aplicacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    aplicacion.OfertaId = id;
                    aplicacion.FechaAplicacion = DateTime.Now;
                    aplicacion.Estado = "Pendiente";

                    _context.Add(aplicacion);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "¡Tu aplicación ha sido enviada con éxito! Nos pondremos en contacto contigo pronto.";

                    return RedirectToAction("Detalles", new { id });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar aplicación: {ex.Message}");
                    ModelState.AddModelError("", "Ocurrió un error al enviar tu aplicación. Por favor, intenta nuevamente.");
                }
            }

            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            ViewData["Oferta"] = oferta;
            return View("Detalles", oferta);
        }
    }
}