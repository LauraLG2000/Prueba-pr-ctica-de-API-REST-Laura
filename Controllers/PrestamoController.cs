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
        new Prestamo(3, "Ana", "Una corte de Niebla y Furia", DateTime.Now, 6, true, EstadoEnum.Activo),
    };

    private static int siguienteId = listaPrestamos.Count() + 1;

    [HttpGet]
    public ActionResult<IEnumerable<Prestamo>> getAllPrestamos()
    {
        return Ok(listaPrestamos);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Prestamo> getPrestamoById(int id)
    {
        Prestamo? prestamoEncontrado = listaPrestamos.FirstOrDefault(prestamo => prestamo.Id == id);

        if(prestamoEncontrado is null) return NotFound($"Error: No se encuentra el préstamo con id {id}");
        
        return Ok(prestamoEncontrado);
    }

    [HttpPost]
    public ActionResult<Prestamo> postPrestamo([FromBody] Prestamo prestamo)
    {
          prestamo.Id = siguienteId;
          prestamo.Estado = EstadoEnum.Activo;

        listaPrestamos.Add(prestamo);

        siguienteId++;

        return CreatedAtAction(nameof(getPrestamoById), new {id = prestamo.Id}, prestamo);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Prestamo> putPrestamo([FromBody] Prestamo prestamo, int id)
    {
        Prestamo? prestamoEncontrado = listaPrestamos.FirstOrDefault(p => p.Id == id);
        if(prestamoEncontrado is null) return NotFound($"Error: No se encuentra el préstamo con id {id}");

        prestamoEncontrado.NombreLector = prestamo.NombreLector;
        prestamoEncontrado.TituloLibro = prestamo.TituloLibro;
        prestamoEncontrado.FechaPrestamo = prestamo.FechaPrestamo;
        prestamoEncontrado.DuracionDias = prestamo.DuracionDias;
        prestamoEncontrado.EsRenovable = prestamo.EsRenovable;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult deletePrestamo(int id)
    {
        Prestamo? prestamoEncontrado = listaPrestamos.FirstOrDefault(p => p.Id == id);
        if(prestamoEncontrado is null) return NotFound($"Error: No se encuentra el préstamo con id {id}");

        listaPrestamos.Remove(prestamoEncontrado);
        return NoContent();
    }

    [HttpPut("{id:int}/devolver")]
    public ActionResult<Prestamo> devolverPrestamo(int id)
    {
        Prestamo? prestamoEncontrado = listaPrestamos.FirstOrDefault(p => p.Id == id);
        if(prestamoEncontrado is null) return NotFound($"Error: No se encuentra el préstamo con id {id}");

        if(prestamoEncontrado.Estado.ToString().Equals("Devuelto")) return Conflict($"Error: El estado del préstamo ya es Devuelto");

        prestamoEncontrado.Estado = EstadoEnum.Devuelto;
        return NoContent();
    }
    
    [HttpGet("recientes")]
    public ActionResult<IEnumerable<Prestamo>> getPrestamosRecientes([FromQuery] int cantidad = 5)
    {
        if(cantidad <= 0) return BadRequest("Error: La cantidad de préstamos a mostrar no puede ser menor o igual que cero");

        return Ok(listaPrestamos
        .OrderByDescending(prestamo => prestamo.FechaPrestamo)
        .Take(cantidad)
        .ToList());
    }
}
