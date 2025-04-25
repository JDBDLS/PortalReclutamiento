using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.PortalReclutamiento.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return contacto;
        }

        
        [HttpPost]
        public async Task<ActionResult<Contacto>> PostContacto(Contacto contacto)
        {
            
            contacto.FechaEnvio = DateTime.Now;

            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContacto), new { id = contacto.Id }, contacto);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}