using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class InformeEncabezadoDto
{
    public int IdInforme { get; set; }
    public int IdUsuario { get; set; }
    public string? Titulo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public string? Estado { get; set; }
    public ICollection<MantenimientoDto>? Mantenimientos { get; set; }
    public ICollection<DetalleInformeDto>? Detalles { get; set; }
    public IEnumerable<BitacoraDto>? Bitacoras { get; set; }
}
