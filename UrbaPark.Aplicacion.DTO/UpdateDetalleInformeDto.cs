using System;
using System.ComponentModel.DataAnnotations;

namespace UrbaPark.Aplicacion.DTO;

public class UpdateDetalleInformeDto
{
    public int? IdInforme { get; set; }
    public string? Descripcion { get; set; }

    
}
