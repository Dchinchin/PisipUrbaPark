using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateInformeEncabezadoDto
{
    [Required(ErrorMessage = "El ID de usuario es obligatorio.")]
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
    public required string Titulo { get; set; }
}
