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
    public class MantenimientoServicioImpl : IMantenimientoServicio
    {
        private IMantenimientoRepositorio mantenimientoRepositorio;

        public MantenimientoServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.mantenimientoRepositorio = new MantenimientoRepositorioImpl(pisip_UrbanParkContext);

        }
        public async Task MantenimientoAddAsync(Mantenimientos TEntity)
        {
            await mantenimientoRepositorio.AddAsync(TEntity);
        }
        public async Task MantenimientoUpdateAsync(Mantenimientos Entity)
        {
            await mantenimientoRepositorio.UpdateAsync(Entity);
        }
        public async Task MantenimientoDeleteAsync(int id)
        {
            await mantenimientoRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Mantenimientos>> MantenimientoGetAllAsync()
        {
            return await mantenimientoRepositorio.GetAllAsync();
        }
        public async Task<Mantenimientos> MantenimientoGetByIdAsync(int id)
        {
            return await mantenimientoRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Mantenimientos>> ListarMantenimientoActivo()
        {
            return await mantenimientoRepositorio.ListarMantenimientoActivo();
        }


    }
}
