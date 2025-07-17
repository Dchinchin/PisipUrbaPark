using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateMantenimientoDto
{
    public int? IdUsuario { get; set; }
    public int? IdParqueadero { get; set; }
    public int? IdTipoMantenimiento { get; set; }
    public int? IdInforme { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }

    [StringLength(80, ErrorMessage = "Las observaciones no pueden exceder los 80 caracteres.")]
    public string? Observaciones { get; set; }
    public string? Estado { get; set; }
}
