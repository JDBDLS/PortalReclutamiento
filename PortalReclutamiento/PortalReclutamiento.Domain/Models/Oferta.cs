using System;
using System.ComponentModel.DataAnnotations;

namespace PortalReclutamiento.PortalReclutamiento.Domain.Models
{
    public class Oferta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La empresa es obligatoria")]
        public string Empresa { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser un valor positivo")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        public DateTime FechaPublicacion { get; set; }
    }
}