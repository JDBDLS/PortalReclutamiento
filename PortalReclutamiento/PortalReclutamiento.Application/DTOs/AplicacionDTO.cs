using System;

namespace PortalReclutamiento.Application.DTOs
{
    public class AplicacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int OfertaId { get; set; }
        public string TituloOferta { get; set; }
        public string EmpresaOferta { get; set; }
        public DateTime FechaAplicacion { get; set; }
        public string Estado { get; set; }
        public string ComentariosAdmin { get; set; }
        public DateTime? FechaCita { get; set; }
    }
}