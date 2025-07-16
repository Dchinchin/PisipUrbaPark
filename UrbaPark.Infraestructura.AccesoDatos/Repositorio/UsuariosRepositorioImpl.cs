using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Dominio.Servicio.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class UsuariosRepositorioImpl : RepositorioImpl<Usuarios>, IUsuariosRepositorio
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;
        private readonly IHashService _hashService;

        public UsuariosRepositorioImpl(Pisip_UrbanParkContext dbContext, IHashService hashService) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
            _hashService = hashService;
        }

        public override async Task AddAsync(Usuarios entity)
        {
            entity.Contrasena = _hashService.HashPassword(entity.Contrasena);
            await base.AddAsync(entity);
        }

        public override async Task UpdateAsync(Usuarios entity)
        {
            // Only hash if the password has changed or is not already hashed
            // A more robust solution would involve checking if the password is a new plaintext password
            // For simplicity, we'll re-hash if it's not already a BCrypt hash (starts with $2a$ or $2b$)
            if (!entity.Contrasena.StartsWith("$2a$") && !entity.Contrasena.StartsWith("$2b$"))
            {
                entity.Contrasena = _hashService.HashPassword(entity.Contrasena);
            }
            await base.UpdateAsync(entity);
        }
    }
}
