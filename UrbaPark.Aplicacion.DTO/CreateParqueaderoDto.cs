using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateParqueaderoDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, ErrorMessage = "El nombre no puede exceder los 80 caracteres.")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(80, ErrorMessage = "La dirección no puede exceder los 80 caracteres.")]
    public required string Direccion { get; set; }

    
}
