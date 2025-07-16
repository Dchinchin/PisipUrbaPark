using System;

namespace UrbaPark.Aplicacion.DTO
{
    public class InformeEncabezadoFilterDto
    {
        public int? IdInforme { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
    }
}
