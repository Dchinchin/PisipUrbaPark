using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateBitacoraDto
{
    [Required(ErrorMessage = "El ID de mantenimiento es obligatorio.")]
    public int IdMantenimiento { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
    public DateTime FechaHora { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    
}
