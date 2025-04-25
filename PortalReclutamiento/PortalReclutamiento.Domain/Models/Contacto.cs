using System;
using System.ComponentModel.DataAnnotations;

namespace PortalReclutamiento.PortalReclutamiento.Domain.Models
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El asunto es obligatorio")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string Mensaje { get; set; }

        public DateTime FechaEnvio { get; set; } = DateTime.Now;
    }
}