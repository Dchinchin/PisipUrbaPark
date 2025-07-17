namespace UrbaPark.Aplicacion.DTO
{
    public class DetalleInformeFilterDto
    {
        public int? IdDetalleInforme { get; set; }
        public int? IdInforme { get; set; }
        public string? Descripcion { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
