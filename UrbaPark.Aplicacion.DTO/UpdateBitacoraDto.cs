using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateBitacoraDto
{
    [Required(ErrorMessage = "El ID de bitácora es obligatorio para la actualización.")]
    public int IdBitacora { get; set; }

    public int? IdInforme { get; set; }
    public int? IdMantenimiento { get; set; }
    public DateTime? FechaHora { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    [StringLength(80, ErrorMessage = "La URL de la imagen no puede exceder los 80 caracteres.")]
    public string? ImagenUrl { get; set; }
}
