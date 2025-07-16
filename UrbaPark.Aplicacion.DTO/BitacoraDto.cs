using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class BitacoraDto
{
    public int IdBitacora { get; set; }
    public int? IdInforme { get; set; }
    public int? IdMantenimiento { get; set; }
    public DateTime? FechaHora { get; set; }
    public string? Descripcion { get; set; }
    public string? ImagenUrl { get; set; }
}
