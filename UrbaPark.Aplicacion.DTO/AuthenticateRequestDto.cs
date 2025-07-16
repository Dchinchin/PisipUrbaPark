namespace UrbaPark.Aplicacion.DTO
{
    public class AuthenticateRequestDto
    {
        public required string Correo { get; set; }
        public required string Contrasena { get; set; }
    }
}