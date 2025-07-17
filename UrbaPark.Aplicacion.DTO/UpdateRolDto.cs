using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateRolDto
{
    [StringLength(80, ErrorMessage = "El nombre del rol no puede exceder los 80 caracteres.")]
    public string? NombreRol { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }
}
