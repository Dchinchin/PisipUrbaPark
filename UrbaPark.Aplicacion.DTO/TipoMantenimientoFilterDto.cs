namespace UrbaPark.Aplicacion.DTO
{
    public class TipoMantenimientoFilterDto
    {
        public int? IdTipoMantenimiento { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
