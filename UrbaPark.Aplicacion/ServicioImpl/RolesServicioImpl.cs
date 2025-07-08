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
        readonly IRolesRepositorio rolesRepositorio;

        public RolesServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.rolesRepositorio = new RolesRepositorioImpl(pisip_UrbanParkContext);
        }

        public Task RolesAddAsync(Roles TEntity)
        {
            throw new NotImplementedException();
        }

        public Task RolesDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Roles>> RolesGetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Roles> RolesGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RolesUpdateAsync(Roles Entity)
        {
            throw new NotImplementedException();
        }
    }
}
