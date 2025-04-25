using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;

namespace PortalReclutamiento.PortalReclutamiento.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Aplicacion> Aplicaciones { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
    }
}