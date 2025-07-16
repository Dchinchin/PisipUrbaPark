using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateInformeEncabezadoDto
{
    [Required(ErrorMessage = "El ID de informe es obligatorio para la actualización.")]
    public int IdInforme { get; set; }

    public int? IdMantenimiento { get; set; }
    public DateTime? Fecha { get; set; }
}
