using PortalReclutamiento.Application.Interfaces;
using PortalReclutamiento.Domain.Interfaces;
using PortalReclutamiento.Domain.Models;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalReclutamiento.Application.Services
{
    public class AplicacionService : IAplicacionService
    {
        private readonly IAplicacionRepository _aplicacionRepository;

        public AplicacionService(IAplicacionRepository aplicacionRepository)
        {
            _aplicacionRepository = aplicacionRepository;
        }

        public async Task<IEnumerable<Aplicacion>> GetAllAsync()
        {
            return await _aplicacionRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Aplicacion>> GetByEmailAsync(string email)
        {
            return await _aplicacionRepository.GetByEmailAsync(email);
        }

        public async Task<Aplicacion> GetByIdAsync(int id)
        {
            return await _aplicacionRepository.GetByIdAsync(id);
        }

        public async Task<Aplicacion> CreateAsync(Aplicacion aplicacion)
        {
            return await _aplicacionRepository.AddAsync(aplicacion);
        }

        public async Task UpdateAsync(Aplicacion aplicacion)
        {
            await _aplicacionRepository.UpdateAsync(aplicacion);
        }

        public async Task UpdateEstadoAsync(int id, string estado, string comentarios)
        {
            await _aplicacionRepository.UpdateEstadoAsync(id, estado, comentarios);
        }
    }
}