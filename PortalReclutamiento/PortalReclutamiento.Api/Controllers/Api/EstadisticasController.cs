using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Persistence.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalReclutamiento.PortalReclutamiento.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadisticasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("ofertas-por-categoria")]
        public async Task<ActionResult<IEnumerable<object>>> GetOfertasPorCategoria()
        {
            var estadisticas = await _context.Ofertas
                .GroupBy(o => o.Categoria)
                .Select(g => new {
                    Categoria = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return estadisticas;
        }

        
        [HttpGet("ofertas-por-ubicacion")]
        public async Task<ActionResult<IEnumerable<object>>> GetOfertasPorUbicacion()
        {
            var estadisticas = await _context.Ofertas
                .GroupBy(o => o.Ubicacion)
                .Select(g => new {
                    Ubicacion = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return estadisticas;
        }

        
        [HttpGet("aplicaciones-por-oferta")]
        public async Task<ActionResult<IEnumerable<object>>> GetAplicacionesPorOferta()
        {
            var estadisticas = await _context.Aplicaciones
                .GroupBy(a => a.OfertaId)
                .Select(g => new {
                    OfertaId = g.Key,
                    Titulo = _context.Ofertas.Where(o => o.Id == g.Key).Select(o => o.Titulo).FirstOrDefault(),
                    Cantidad = g.Count()
                })
                .ToListAsync();

            return estadisticas;
        }

        
        [HttpGet("resumen")]
        public async Task<ActionResult<object>> GetResumen()
        {
            var totalOfertas = await _context.Ofertas.CountAsync();
            var totalAplicaciones = await _context.Aplicaciones.CountAsync();
            var totalContactos = await _context.Contactos.CountAsync();
            var ofertasRecientes = await _context.Ofertas
                .OrderByDescending(o => o.FechaPublicacion)
                .Take(5)
                .Select(o => new { o.Id, o.Titulo, o.Empresa, o.FechaPublicacion })
                .ToListAsync();

            return new
            {
                TotalOfertas = totalOfertas,
                TotalAplicaciones = totalAplicaciones,
                TotalContactos = totalContactos,
                OfertasRecientes = ofertasRecientes
            };
        }
    }
}