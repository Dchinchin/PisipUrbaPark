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
    public class ParqueaderoServicioImpl : IParqueaderoServicio
    {
      private IParqueaderoRepositorio parqueaderoRepositorio;
        
        public ParqueaderoServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.parqueaderoRepositorio = new ParqueaderoRepositorioImpl(pisip_UrbanParkContext);
        }
        public async Task ParqueaderoAddAsync(Parquadero TEntity)
        {
            await parqueaderoRepositorio.AddAsync(TEntity);
        }
        public async Task ParqueaderoUpdateAsync(Parquadero Entity)
        {
            await parqueaderoRepositorio.UpdateAsync(Entity);
        }

        public async Task ParqueaderoDeleteAsync(int id)
        {
            await parqueaderoRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Parquadero>> ParqueaderoGetAllAsync()
        {
            return await parqueaderoRepositorio.GetAllAsync();
        }
        public async Task<Parquadero> ParqueaderoGetByIdAsync(int id)
        {
            return await parqueaderoRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Parquadero>> ListarParqueaderoActivo()
        {
            return await parqueaderoRepositorio.ListarParqueaderoActivo();
        }

    }
}
