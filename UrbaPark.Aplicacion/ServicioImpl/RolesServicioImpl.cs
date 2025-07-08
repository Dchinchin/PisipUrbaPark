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
    public class RolesServicioImpl : IRolesServicio
    {
        private IRolesRepositorio rolesRepositorio;
        public RolesServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.rolesRepositorio = new RolesRepositorioImpl(pisip_UrbanParkContext);
        }
       
        public async Task RolesUpdateAsync(Roles Entity)
        {
            await rolesRepositorio.UpdateAsync(Entity);
        }
        public async Task RolesDeleteAsync(int id)
        {
            await rolesRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Roles>> RolesGetAllAsync()
        {
            return await rolesRepositorio.GetAllAsync();
        }
        public async Task<Roles> RolesGetByIdAsync(int id)
        {
            return await rolesRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Roles>> ListarRolesActivo()
        {
            return await rolesRepositorio.ListarRolesActivo();
        }

        public async Task RolesAddAsync(Roles TEntity)
        {
            await rolesRepositorio.AddAsync(TEntity);
        }

           }

}
