using PortalReclutamiento.Application.Interfaces;
using PortalReclutamiento.Domain.Interfaces;
using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _planRepository.GetAllAsync();
        }

        public async Task<Plan> GetByIdAsync(int id)
        {
            return await _planRepository.GetByIdAsync(id);
        }

        public async Task<Plan> CreateAsync(Plan plan)
        {
            return await _planRepository.AddAsync(plan);
        }

        public async Task UpdateAsync(Plan plan)
        {
            await _planRepository.UpdateAsync(plan);
        }

        public async Task DeleteAsync(int id)
        {
            await _planRepository.DeleteAsync(id);
        }
    }
}