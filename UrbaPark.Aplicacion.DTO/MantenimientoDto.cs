using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class MantenimientoDto
{
    public int IdMantenimiento { get; set; }
    public int? IdUsuario { get; set; }
    public int? IdParqueadero { get; set; }
    public int? IdTipomantenimiento { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Observaciones { get; set; }
}
