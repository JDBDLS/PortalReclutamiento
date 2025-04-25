using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Persistence.Data;

namespace PortalReclutamiento.PortalReclutamiento.Web.Controllers
{
    public class AplicacionesAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AplicacionesAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            try
            {
                var aplicaciones = await _context.Aplicaciones
                    .Include(a => a.Oferta)
                    .ToListAsync();
                return View(aplicaciones);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al obtener aplicaciones: {ex.Message}");

               
                ViewBag.ErrorMessage = "Ocurrió un error al cargar las solicitudes. Por favor, intenta nuevamente más tarde.";
                return View(new List<Aplicacion>());
            }
        }

        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacion = await _context.Aplicaciones
                .Include(a => a.Oferta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacion == null)
            {
                return NotFound();
            }

            return View(aplicacion);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacion = await _context.Aplicaciones
                .Include(a => a.Oferta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacion == null)
            {
                return NotFound();
            }

            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                new SelectListItem { Value = "Revisión", Text = "Revisión" },
                new SelectListItem { Value = "Entrevista", Text = "Entrevista" },
                new SelectListItem { Value = "Contratado", Text = "Contratado" },
                new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
            };

            return View(aplicacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,OfertaId,Nombre,Email,Telefono,FechaAplicacion,Estado,ComentariosAdmin")] Aplicacion aplicacion)
        {
            if (id != aplicacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aplicacionOriginal = await _context.Aplicaciones.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

                    _context.Entry(aplicacion).Property(x => x.Estado).IsModified = true;
                    _context.Entry(aplicacion).Property(x => x.ComentariosAdmin).IsModified = true;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AplicacionExists(aplicacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                new SelectListItem { Value = "Revisión", Text = "Revisión" },
                new SelectListItem { Value = "Entrevista", Text = "Entrevista" },
                new SelectListItem { Value = "Contratado", Text = "Contratado" },
                new SelectListItem { Value = "Rechazado", Text = "Rechazado" }
            };

            return View(aplicacion);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aplicacion = await _context.Aplicaciones
                .Include(a => a.Oferta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aplicacion == null)
            {
                return NotFound();
            }

            return View(aplicacion);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                var aplicacion = await _context.Aplicaciones.FindAsync(id);
                if (aplicacion != null)
                {
                    _context.Aplicaciones.Remove(aplicacion);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al eliminar aplicación: {ex.Message}");

                
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar la solicitud. Por favor, intenta nuevamente.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult IniciarSesion(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("MisAplicaciones");
            }

            HttpContext.Session.SetString("UserEmail", email);

            return RedirectToAction("MisAplicaciones");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("UserEmail");
            return RedirectToAction("MisAplicaciones");
        }

        public async Task<IActionResult> MisAplicaciones()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(email))
            {
                return View("SolicitarEmail");
            }

            try
            {
                var aplicaciones = await _context.Aplicaciones
                    .Include(a => a.Oferta)
                    .Where(a => a.Email == email)
                    .ToListAsync();

                ViewBag.UserEmail = email;
                return View(aplicaciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener aplicaciones: {ex.Message}");

                ViewBag.UserEmail = email;
                ViewBag.ErrorMessage = "Ocurrió un error al cargar tus solicitudes. Por favor, intenta nuevamente más tarde.";
                return View(new List<Aplicacion>());
            }
        }

        private bool AplicacionExists(int id)
        {
            return _context.Aplicaciones.Any(e => e.Id == id);
        }
    }
}