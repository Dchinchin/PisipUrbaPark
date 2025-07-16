using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateInformeEncabezadoDto
{
    [Required(ErrorMessage = "El ID de mantenimiento es obligatorio.")]
    public int IdMantenimiento { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime Fecha { get; set; }
}
