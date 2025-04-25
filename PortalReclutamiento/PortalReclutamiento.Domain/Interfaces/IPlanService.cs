using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Application.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllAsync();
        Task<Plan> GetByIdAsync(int id);
        Task<Plan> CreateAsync(Plan plan);
        Task UpdateAsync(Plan plan);
        Task DeleteAsync(int id);
    }
}