using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalReclutamiento.PortalReclutamiento.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AplicacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aplicacion>>> GetAplicaciones()
        {
            return await _context.Aplicaciones
                .Include(a => a.Oferta)
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Aplicacion>> GetAplicacion(int id)
        {
            var aplicacion = await _context.Aplicaciones
                .Include(a => a.Oferta)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aplicacion == null)
            {
                return NotFound();
            }

            return aplicacion;
        }

        
        [HttpPost]
        public async Task<ActionResult<Aplicacion>> PostAplicacion(Aplicacion aplicacion)
        {
            // Verificar que la oferta existe
            var oferta = await _context.Ofertas.FindAsync(aplicacion.OfertaId);
            if (oferta == null)
            {
                return BadRequest("La oferta especificada no existe.");
            }

            
            aplicacion.FechaAplicacion = DateTime.Now;

            _context.Aplicaciones.Add(aplicacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAplicacion), new { id = aplicacion.Id }, aplicacion);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAplicacion(int id, Aplicacion aplicacion)
        {
            if (id != aplicacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(aplicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AplicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAplicacion(int id)
        {
            var aplicacion = await _context.Aplicaciones.FindAsync(id);
            if (aplicacion == null)
            {
                return NotFound();
            }

            _context.Aplicaciones.Remove(aplicacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("oferta/{ofertaId}")]
        public async Task<ActionResult<IEnumerable<Aplicacion>>> GetAplicacionesByOferta(int ofertaId)
        {
            
            var oferta = await _context.Ofertas.FindAsync(ofertaId);
            if (oferta == null)
            {
                return NotFound("La oferta especificada no existe.");
            }

            var aplicaciones = await _context.Aplicaciones
                .Where(a => a.OfertaId == ofertaId)
                .ToListAsync();

            return aplicaciones;
        }

        private bool AplicacionExists(int id)
        {
            return _context.Aplicaciones.Any(e => e.Id == id);
        }
    }
}