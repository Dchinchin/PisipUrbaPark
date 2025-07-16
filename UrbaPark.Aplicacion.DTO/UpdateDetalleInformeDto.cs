using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateDetalleInformeDto
{
    [Required(ErrorMessage = "El ID de detalle de informe es obligatorio para la actualización.")]
    public int IdDetInfo { get; set; }

    public int? IdInforme { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    [StringLength(80, ErrorMessage = "La URL del archivo no puede exceder los 80 caracteres.")]
    public string? ArchivoUrl { get; set; }
}
