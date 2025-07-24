using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UsuarioDto
{
    public int IdUsuario { get; set; }
    public int? IdRol { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Correo { get; set; }
    public string? Cedula { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool EstaEliminado { get; set; }
    public bool ContrasenaActualizada { get; set; }
}
