using System.ComponentModel.DataAnnotations;

namespace PortalReclutamiento.PortalReclutamiento.Domain.Models
{
    public class Plan
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public string Caracteristicas { get; set; }
    }
}