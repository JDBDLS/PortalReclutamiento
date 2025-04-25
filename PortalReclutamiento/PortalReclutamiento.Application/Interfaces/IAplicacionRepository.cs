using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Domain.Interfaces
{
    public interface IAplicacionRepository
    {
        Task<IEnumerable<Aplicacion>> GetAllAsync();
        Task<Aplicacion> GetByIdAsync(int id);
        Task<IEnumerable<Aplicacion>> GetByEmailAsync(string email);
        Task<IEnumerable<Aplicacion>> GetByOfertaIdAsync(int ofertaId);
        Task<Aplicacion> AddAsync(Aplicacion aplicacion);
        Task UpdateAsync(Aplicacion aplicacion);
        Task DeleteAsync(int id);
        Task UpdateEstadoAsync(int id, string estado, string comentarios);
    }
}