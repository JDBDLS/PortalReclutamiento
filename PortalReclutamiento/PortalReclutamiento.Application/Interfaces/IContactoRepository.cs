using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Domain.Interfaces
{
    public interface IContactoRepository
    {
        Task<IEnumerable<Contacto>> GetAllAsync();
        Task<Contacto> GetByIdAsync(int id);
        Task<Contacto> AddAsync(Contacto contacto);
        Task UpdateAsync(Contacto contacto);
        Task DeleteAsync(int id);
    }
}