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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Ofertas()
        {
            try
            {
                var ofertas = await _context.Ofertas.ToListAsync();
                return View(ofertas);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar las ofertas. Por favor, intenta nuevamente más tarde.";
                return View(new List<Oferta>());
            }
        }


        public async Task<IActionResult> DetallesOferta(int? id)
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
                return RedirectToAction(nameof(Ofertas));
            }
        }


        public async Task<IActionResult> Planes()
        {
            try
            {
                var planes = await _context.Planes.ToListAsync();
                return View(planes);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al cargar los planes. Por favor, intenta nuevamente más tarde.";
                return View(new List<Plan>());
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelarPlan()
        {
            try
            {
                TempData["SuccessMessage"] = "Tu plan ha sido cancelado. Seguirá activo hasta el final del período de facturación.";
                return RedirectToAction(nameof(Planes));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al cancelar el plan. Por favor, intenta nuevamente más tarde.";
                return RedirectToAction(nameof(Planes));
            }
        }
    }
}