using Microsoft.AspNetCore.Mvc;

namespace Prueba_pr_ctica_de_API_REST_Laura.Controllers;
using Prueba_pr_ctica_de_API_REST_Laura.Models;

[ApiController]
[Route("[controller]")]
public class PrestamoController : ControllerBase
{
    public static List<Prestamo> listaPrestamos = new()
    {
        new Prestamo(1, "Laura", "Una corte de Rosas y Espinas", DateTime.Now, 10, true, EstadoEnum.Activo),
        new Prestamo(2, "Javier", "La Casa en el Mar Más Azul", DateTime.Now, 3, false, EstadoEnum.Activo),
        new Prestamo(3, "Ana", "Una corte de Niebla y Furia", DateTime.Now, 6, true, EstadoEnum.Devuelto),
    };
}