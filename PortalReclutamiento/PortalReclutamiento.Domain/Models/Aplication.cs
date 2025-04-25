using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalReclutamiento.PortalReclutamiento.Domain.Models
{
    public class Aplicacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; }

        [Required]
        public int OfertaId { get; set; }

        [ForeignKey("OfertaId")]
        public virtual Oferta Oferta { get; set; }

        public DateTime FechaAplicacion { get; set; }

      
        public string Estado { get; set; }

      
        public string ComentariosAdmin { get; set; }

       
        public DateTime? FechaCita { get; set; }
    }
}