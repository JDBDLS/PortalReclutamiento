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
    public class CitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            try
            {
               
                var aplicaciones = await _context.Aplicaciones
                    .Include(a => a.Oferta)
                    .Where(a => a.Estado == "Pendiente" || a.Estado == "En Proceso")
                    .ToListAsync();

                return View(aplicaciones);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar las solicitudes. Por favor, intenta nuevamente más tarde.";
                return View(new List<Aplicacion>());
            }
        }

        
        public async Task<IActionResult> Agendar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var aplicacion = await _context.Aplicaciones
                    .Include(a => a.Oferta)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (aplicacion == null)
                {
                    return NotFound();
                }

                return View(aplicacion);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agendar(int id, [Bind("Id,FechaCita,ComentariosAdmin")] Aplicacion aplicacionUpdate)
        {
            if (id != aplicacionUpdate.Id)
            {
                return NotFound();
            }

            try
            {
                var aplicacion = await _context.Aplicaciones.FindAsync(id);
                if (aplicacion == null)
                {
                    return NotFound();
                }

               
                aplicacion.FechaCita = aplicacionUpdate.FechaCita;
                aplicacion.ComentariosAdmin = aplicacionUpdate.ComentariosAdmin;
                aplicacion.Estado = "Entrevista Agendada";

                _context.Update(aplicacion);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Entrevista agendada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al agendar la entrevista.";
                return RedirectToAction(nameof(Agendar), new { id });
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
                var aplicacion = await _context.Aplicaciones
                    .Include(a => a.Oferta)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (aplicacion == null)
                {
                    return NotFound();
                }

                return View(aplicacion);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}