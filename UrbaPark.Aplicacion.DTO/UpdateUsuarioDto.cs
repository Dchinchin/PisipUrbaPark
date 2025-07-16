using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateUsuarioDto
{
    [Required(ErrorMessage = "El ID de usuario es obligatorio para la actualización.")]
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    [StringLength(80, ErrorMessage = "El nombre no puede exceder los 80 caracteres.")]
    public string? Nombre { get; set; }

    [StringLength(80, ErrorMessage = "El apellido no puede exceder los 80 caracteres.")]
    public string? Apellido { get; set; }

    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    [StringLength(80, ErrorMessage = "El correo no puede exceder los 80 caracteres.")]
    public string? Correo { get; set; }

    [StringLength(80, ErrorMessage = "El estado no puede exceder los 80 caracteres.")]
    public string? Estado { get; set; }

    [StringLength(10, MinimumLength = 10, ErrorMessage = "La cédula debe tener 10 caracteres.")]
    public string? Cedula { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Contrasena { get; set; }
}
