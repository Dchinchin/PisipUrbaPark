using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class ParqueaderoDto
{
    public int IdParqueadero { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Estado { get; set; }
}
