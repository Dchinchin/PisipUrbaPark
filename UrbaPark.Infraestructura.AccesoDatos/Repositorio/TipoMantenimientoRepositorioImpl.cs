using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Infraestructura.AccesoDatos;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio;

public class TipoMantenimientoRepositorioImpl : RepositorioImpl<TipoMantenimiento>, ITipoMantenimientoRepositorio
{
    private readonly Pisip_UrbanParkContext _UrbanParkContext;

    public TipoMantenimientoRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
    {
        _UrbanParkContext = dbContext;
    }
}
