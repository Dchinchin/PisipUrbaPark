using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class CreateUsuarioDto
{
    [Required(ErrorMessage = "El rol es obligatorio.")]
    public int IdRol { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(80, ErrorMessage = "El nombre no puede exceder los 80 caracteres.")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(80, ErrorMessage = "El apellido no puede exceder los 80 caracteres.")]
    public required string Apellido { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    [StringLength(80, ErrorMessage = "El correo no puede exceder los 80 caracteres.")]
    public required string Correo { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria.")]
    [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 caracteres.")]
    public required string Cedula { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public required string Contrasena { get; set; }
    public bool ContrasenaActualizada { get; set; }
}
