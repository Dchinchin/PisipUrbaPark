using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateDetalleInformeDto
{
    public int? IdInforme { get; set; }

    [StringLength(80, ErrorMessage = "La descripción no puede exceder los 80 caracteres.")]
    public string? Descripcion { get; set; }

    
}
