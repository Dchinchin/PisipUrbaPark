using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class DetalleInformeDto
{
    public int IdDetInfo { get; set; }
    public int? IdInforme { get; set; }
    public string? Descripcion { get; set; }
    public string? ArchivoUrl { get; set; }
}
