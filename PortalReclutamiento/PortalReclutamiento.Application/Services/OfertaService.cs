using PortalReclutamiento.Application.Interfaces;
using PortalReclutamiento.Domain.Interfaces;
using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Application.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly IOfertaRepository _ofertaRepository;

        public OfertaService(IOfertaRepository ofertaRepository)
        {
            _ofertaRepository = ofertaRepository;
        }

        public async Task<IEnumerable<Oferta>> GetAllAsync()
        {
            return await _ofertaRepository.GetAllAsync();
        }

        public async Task<Oferta> GetByIdAsync(int id)
        {
            return await _ofertaRepository.GetByIdAsync(id);
        }

        public async Task<Oferta> CreateAsync(Oferta oferta)
        {
            return await _ofertaRepository.AddAsync(oferta);
        }

        public async Task UpdateAsync(Oferta oferta)
        {
            await _ofertaRepository.UpdateAsync(oferta);
        }

        public async Task DeleteAsync(int id)
        {
            await _ofertaRepository.DeleteAsync(id);
        }
    }
}