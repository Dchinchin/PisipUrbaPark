using System;

namespace UrbaPark.Aplicacion.DTO
{
    public class MantenimientoFilterDto
    {
        public int? IdMantenimiento { get; set; }
        public int? IdParqueadero { get; set; }
        public int? IdTipoMantenimiento { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdInforme { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string? Descripcion { get; set; }
        public bool? EstaEliminado { get; set; }
        public string? Estado { get; set; }
    }
}
