using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateMantenimientoDto
{
    [Required(ErrorMessage = "El ID de mantenimiento es obligatorio para la actualización.")]
    public int IdMantenimiento { get; set; }

    public int? IdUsuario { get; set; }
    public int? IdParqueadero { get; set; }
    public int? IdTipomantenimiento { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaFin { get; set; }

    [StringLength(80, ErrorMessage = "Las observaciones no pueden exceder los 80 caracteres.")]
    public string? Observaciones { get; set; }
}
