using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Aplicacion.Servicio;
using UrbaPark.Dominio.Modelo.Abstracciones;
using UrbaPark.Infraestructura.AccesoDatos;
using UrbaPark.Infraestructura.AccesoDatos.Repositorio;

namespace UrbaPark.Aplicacion.ServicioImpl
{
    public class UsuariosServicioImpl : IUsuariosServicio
    {
        private IUsuariosRepositorio usuariosRepositorio;
        public UsuariosServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.usuariosRepositorio = new UsuariosRepositorioImpl(pisip_UrbanParkContext);
        }
        public async Task UsuariosAddAsync(Usuarios TEntity)
        {
            await usuariosRepositorio.AddAsync(TEntity);
        }
        public async Task UsuariosUpdateAsync(Usuarios Entity)
        {
            await usuariosRepositorio.UpdateAsync(Entity);
        }
        public async Task UsuariosDeleteAsync(int id)
        {
            await usuariosRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Usuarios>> UsuariosGetAllAsync()
        {
            return await usuariosRepositorio.GetAllAsync();
        }
        public async Task<Usuarios> UsuariosGetByIdAsync(int id)
        {
            return await usuariosRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Usuarios>> ListarUsuariosActivo()
        {
            return await usuariosRepositorio.ListarUsuariosActivo();
        }

    }
}
