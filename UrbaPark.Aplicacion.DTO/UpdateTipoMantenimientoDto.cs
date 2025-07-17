using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateTipoMantenimientoDto
{
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
    public string? Descripcion { get; set; }
}
