using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateParqueaderoDto
{
    [StringLength(80, ErrorMessage = "El nombre no puede exceder los 80 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(80, ErrorMessage = "La dirección no puede exceder los 80 caracteres.")]
    public string? Direccion { get; set; }

    
}
