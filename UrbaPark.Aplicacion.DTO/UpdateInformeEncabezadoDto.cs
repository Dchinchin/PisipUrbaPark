using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateInformeEncabezadoDto
{
    [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
    public string? Titulo { get; set; }
}
