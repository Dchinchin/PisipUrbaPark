using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateRolDto
{
    [Required(ErrorMessage = "El ID del rol es obligatorio para la actualización.")]
    public int IdRol { get; set; }

    [StringLength(80, ErrorMessage = "El nombre del rol no puede exceder los 80 caracteres.")]
    public string? NombreRol { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }
}
