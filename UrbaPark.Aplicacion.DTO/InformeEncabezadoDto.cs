using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class InformeEncabezadoDto
{
    public int IdInforme { get; set; }
    public int? IdMantenimiento { get; set; }
    public DateTime? Fecha { get; set; }
}
