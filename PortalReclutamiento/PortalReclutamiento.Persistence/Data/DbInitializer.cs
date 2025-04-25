using Microsoft.EntityFrameworkCore;
using PortalReclutamiento.PortalReclutamiento.Domain.Models;
using System;
using System.Linq;

namespace PortalReclutamiento.PortalReclutamiento.Persistence.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Ofertas.Any())
                {
                    return;
                }

                var ofertas = new Oferta[]
                {
                    new Oferta
                    {
                        Titulo = "Desarrollador Full Stack",
                        Empresa = "TechSolutions",
                        Ubicacion = "Ciudad de México",
                        Salario = 35000,
                        Categoria = "Tecnología",
                        Descripcion = "Buscamos un desarrollador Full Stack con experiencia en .NET Core y React para unirse a nuestro equipo de desarrollo. El candidato ideal debe tener al menos 3 años de experiencia en desarrollo web.",
                        FechaPublicacion = DateTime.Now.AddDays(-5)
                    },
                    new Oferta
                    {
                        Titulo = "Especialista en Marketing Digital",
                        Empresa = "MarketPro",
                        Ubicacion = "Guadalajara",
                        Salario = 28000,
                        Categoria = "Marketing",
                        Descripcion = "Estamos buscando un especialista en marketing digital con experiencia en SEO, SEM y redes sociales. Deberá gestionar campañas publicitarias y analizar resultados.",
                        FechaPublicacion = DateTime.Now.AddDays(-3)
                    },
                    new Oferta
                    {
                        Titulo = "Gerente de Ventas",
                        Empresa = "ComercialMex",
                        Ubicacion = "Monterrey",
                        Salario = 40000,
                        Categoria = "Ventas",
                        Descripcion = "Buscamos un gerente de ventas con experiencia en liderazgo de equipos comerciales. Responsable de establecer estrategias de ventas y cumplir objetivos.",
                        FechaPublicacion = DateTime.Now.AddDays(-7)
                    },
                    new Oferta
                    {
                        Titulo = "Diseñador UX/UI",
                        Empresa = "DesignStudio",
                        Ubicacion = "Ciudad de México",
                        Salario = 32000,
                        Categoria = "Diseño",
                        Descripcion = "Estamos en búsqueda de un diseñador UX/UI con experiencia en la creación de interfaces intuitivas y atractivas. Conocimientos en Figma y Adobe XD.",
                        FechaPublicacion = DateTime.Now.AddDays(-2)
                    },
                    new Oferta
                    {
                        Titulo = "Contador Senior",
                        Empresa = "FinanzasCorp",
                        Ubicacion = "Puebla",
                        Salario = 30000,
                        Categoria = "Finanzas",
                        Descripcion = "Buscamos un contador senior con experiencia en contabilidad general, impuestos y reportes financieros. Conocimientos en SAP y Excel avanzado.",
                        FechaPublicacion = DateTime.Now.AddDays(-10)
                    },
                    new Oferta
                    {
                        Titulo = "Reclutador IT",
                        Empresa = "TalentHunters",
                        Ubicacion = "Guadalajara",
                        Salario = 25000,
                        Categoria = "Recursos Humanos",
                        Descripcion = "Estamos buscando un reclutador especializado en perfiles IT. Experiencia en procesos de selección técnicos y conocimiento del mercado tecnológico.",
                        FechaPublicacion = DateTime.Now.AddDays(-1)
                    }
                };

                context.Ofertas.AddRange(ofertas);
                context.SaveChanges();

                var planes = new Plan[]
                {
                    new Plan
                    {
                        Nombre = "Básico",
                        Precio = 99,
                        Descripcion = "Ideal para pequeñas empresas",
                        Caracteristicas = "5 publicaciones de empleo, 30 días de visibilidad, Soporte por email, Estadísticas básicas, Filtrado de candidatos"
                    },
                    new Plan
                    {
                        Nombre = "Profesional",
                        Precio = 199,
                        Descripcion = "Para empresas en crecimiento",
                        Caracteristicas = "15 publicaciones de empleo, 60 días de visibilidad, Destacado en búsquedas, Soporte prioritario, Estadísticas avanzadas, Herramientas de filtrado avanzadas"
                    },
                    new Plan
                    {
                        Nombre = "Empresarial",
                        Precio = 399,
                        Descripcion = "Para grandes corporaciones",
                        Caracteristicas = "Publicaciones ilimitadas, 90 días de visibilidad, Posición destacada, Acceso a CV premium, Soporte dedicado 24/7, Herramientas de análisis avanzadas, API para integración con sistemas propios"
                    }
                };

                context.Planes.AddRange(planes);
                context.SaveChanges();
            }
        }
    }
}