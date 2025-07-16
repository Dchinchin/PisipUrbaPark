using BCrypt.Net;
using UrbaPark.Dominio.Servicio.Abstracciones;

namespace UrbaPark.Dominio.Servicio.Implementaciones
{
    public class HashService : IHashService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
