using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Dominio.Modelo.Entidades;
using UrbaPark.Infraestructura.AccesoDatos;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio;

public class Det_InfoEncaRepositorioImpl : RepositorioImpl<Detalle_Informe>, IDet_InfoEncaRepositorio
{
    private readonly Pisip_UrbanParkContext _UrbanParkContext;

    public Det_InfoEncaRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
    {
        _UrbanParkContext = dbContext;
    }
}