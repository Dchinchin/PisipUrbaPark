using System;

namespace UrbaPark.Aplicacion.DTO
{
    public class MantenimientoFilterDto
    {
        public int? IdMantenimiento { get; set; }
        public int? IdParqueadero { get; set; }
        public int? IdTipoMantenimiento { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string? Descripcion { get; set; }
        
    }
}
