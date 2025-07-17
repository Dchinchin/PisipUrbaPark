using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class TipoMantenimientoDto
{
    public int IdTipo { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool Estado { get; set; }
}
