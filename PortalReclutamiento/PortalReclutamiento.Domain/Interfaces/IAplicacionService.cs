using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Application.Interfaces
{
    public interface IAplicacionService
    {
        Task<IEnumerable<Aplicacion>> GetAllAsync();
        Task<IEnumerable<Aplicacion>> GetByEmailAsync(string email);
        Task<Aplicacion> GetByIdAsync(int id);
        Task<Aplicacion> CreateAsync(Aplicacion aplicacion);
        Task UpdateAsync(Aplicacion aplicacion);
        Task UpdateEstadoAsync(int id, string estado, string comentarios);
    }
}