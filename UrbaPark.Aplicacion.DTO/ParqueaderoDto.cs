using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class ParqueaderoDto
{
    public int IdParqueadero { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool Estado { get; set; }
}
