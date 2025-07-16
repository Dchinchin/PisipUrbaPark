using System;

namespace UrbaPark.Aplicacion.DTO
{
    public class BitacoraFilterDto
    {
        public int? IdBitacora { get; set; }
        public int? IdInforme { get; set; }
        public int? IdMantenimiento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string? Descripcion { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
