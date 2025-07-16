using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateDetalleInformeDto
{
    [Required(ErrorMessage = "El ID de informe es obligatorio.")]
    public int IdInforme { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    [StringLength(80, ErrorMessage = "La URL del archivo no puede exceder los 80 caracteres.")]
    public string? ArchivoUrl { get; set; }
}
