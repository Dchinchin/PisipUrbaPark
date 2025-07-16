using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class RolDto
{
    public int IdRol { get; set; }
    public string? NombreRol { get; set; }
    public string? Descripcion { get; set; }
}
