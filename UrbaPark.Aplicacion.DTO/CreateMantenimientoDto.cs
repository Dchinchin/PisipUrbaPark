using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateMantenimientoDto
{
    [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El ID de parqueadero es obligatorio.")]
    public int IdParqueadero { get; set; }

    [Required(ErrorMessage = "El ID de tipo de mantenimiento es obligatorio.")]
    public int IdTipoMantenimiento { get; set; }
    

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    public DateTime FechaInicio { get; set; }
    
    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    public DateTime? FechaFin { get; set; }

    [StringLength(80, ErrorMessage = "Las observaciones no pueden exceder los 80 caracteres.")]
    public string? Observaciones { get; set; }
}
