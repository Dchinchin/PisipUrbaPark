using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateBitacoraDto
{

    public int? IdInforme { get; set; }
    public int? IdMantenimiento { get; set; }
    public DateTime? FechaHora { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    
}
