namespace UrbaPark.Aplicacion.DTO
{
    public class UsuarioFilterDto
    {
        public int? IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CorreoElectronico { get; set; }
        public bool? EstaEliminado { get; set; }
    }
}
