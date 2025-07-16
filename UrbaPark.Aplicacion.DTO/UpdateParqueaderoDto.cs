using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateParqueaderoDto
{
    [Required(ErrorMessage = "El ID del parqueadero es obligatorio para la actualización.")]
    public int IdParqueadero { get; set; }

    [StringLength(80, ErrorMessage = "El nombre no puede exceder los 80 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(80, ErrorMessage = "La dirección no puede exceder los 80 caracteres.")]
    public string? Direccion { get; set; }

    
}
