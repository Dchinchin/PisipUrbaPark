using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class MantenimientoDto
{
    public int IdMantenimiento { get; set; }
    public int? IdUsuario { get; set; }
    public int? IdParqueadero { get; set; }
    public int? IdTipoMantenimiento { get; set; }
    public int? IdInforme { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaFin { get; set; }
    public string? Observaciones { get; set; }
    public string? Estado { get; set; }
    public bool? EstaEliminado { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public ICollection<BitacoraDto> Bitacoras { get; set; } = new List<BitacoraDto>();
}
