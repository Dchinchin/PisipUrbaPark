using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateInformeEncabezadoDto
{
    public int? IdMantenimiento { get; set; }
    public DateTime? Fecha { get; set; }
}
