using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Domain.Interfaces
{
    public interface IOfertaRepository
    {
        Task<IEnumerable<Oferta>> GetAllAsync();
        Task<Oferta> GetByIdAsync(int id);
        Task<Oferta> AddAsync(Oferta oferta);
        Task UpdateAsync(Oferta oferta);
        Task DeleteAsync(int id);
    }
}