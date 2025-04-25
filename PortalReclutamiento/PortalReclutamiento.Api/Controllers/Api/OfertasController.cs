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
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OfertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOfertas()
        {
            return await _context.Ofertas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOferta(int id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);

            if (oferta == null)
            {
                return NotFound();
            }

            return oferta;
        }

       
        [HttpPost]
        public async Task<ActionResult<Oferta>> PostOferta(Oferta oferta)
        {
           
            if (oferta.FechaPublicacion == default)
            {
                oferta.FechaPublicacion = DateTime.Now;
            }

            _context.Ofertas.Add(oferta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOferta), new { id = oferta.Id }, oferta);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferta(int id, Oferta oferta)
        {
            if (id != oferta.Id)
            {
                return BadRequest();
            }

            _context.Entry(oferta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfertaExists(id))
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
        public async Task<IActionResult> DeleteOferta(int id)
        {
            var oferta = await _context.Ofertas.FindAsync(id);
            if (oferta == null)
            {
                return NotFound();
            }

            _context.Ofertas.Remove(oferta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOfertasByCategoria(string categoria)
        {
            var ofertas = await _context.Ofertas
                .Where(o => o.Categoria == categoria)
                .ToListAsync();

            return ofertas;
        }

        
        [HttpGet("ubicacion/{ubicacion}")]
        public async Task<ActionResult<IEnumerable<Oferta>>> GetOfertasByUbicacion(string ubicacion)
        {
            var ofertas = await _context.Ofertas
                .Where(o => o.Ubicacion == ubicacion)
                .ToListAsync();

            return ofertas;
        }

        
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Oferta>>> BuscarOfertas(
            [FromQuery] string q = null,
            [FromQuery] string categoria = null,
            [FromQuery] string ubicacion = null)
        {
            var query = _context.Ofertas.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(o =>
                    o.Titulo.Contains(q) ||
                    o.Descripcion.Contains(q) ||
                    o.Empresa.Contains(q));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(o => o.Categoria == categoria);
            }

            if (!string.IsNullOrEmpty(ubicacion))
            {
                query = query.Where(o => o.Ubicacion == ubicacion);
            }

            return await query.ToListAsync();
        }

        private bool OfertaExists(int id)
        {
            return _context.Ofertas.Any(e => e.Id == id);
        }
    }
}