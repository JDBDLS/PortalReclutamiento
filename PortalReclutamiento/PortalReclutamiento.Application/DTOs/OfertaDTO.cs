using System;

namespace PortalReclutamiento.Application.DTOs
{
    public class OfertaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Ubicacion { get; set; }
        public decimal Salario { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}