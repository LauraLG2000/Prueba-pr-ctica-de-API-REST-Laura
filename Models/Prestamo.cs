namespace Prueba_pr_ctica_de_API_REST_Laura.Models;

public class Prestamo
{
    public int Id{get;set;}
    public string NombreLector{get;set;}
    public string TituloLibro{get;set;}
    public DateTime FechaPrestamo{get;set;}
    public int DuracionDias{get;set;}
    public bool EsRenovable{get;set;}
    public EstadoEnum Estado{get;set;} = EstadoEnum.Activo;

    public Prestamo()
    {
        
    }

    public Prestamo(int _id, string _nombreLector, string _tituloLibro, DateTime _fechaPrestamo, int _duracionDias, bool _esRenovable, EstadoEnum _estado)
    {
        this.Id = _id;
        this.NombreLector = _nombreLector;
        this.TituloLibro = _tituloLibro;
        this.FechaPrestamo = _fechaPrestamo;
        this.DuracionDias = _duracionDias;
        this.EsRenovable = _esRenovable;
        this.Estado = _estado;
    }
}