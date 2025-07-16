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
    public string? Estado { get; set; }
    public string? Cedula { get; set; }
}
