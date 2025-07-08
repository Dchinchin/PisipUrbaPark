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
    public class Info_EncaServicioImpl : IInfo_EncaServicio
    {
        private IInfo_EncaRepositorio infoEncaRepositorio;

        public Info_EncaServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.infoEncaRepositorio = new Info_EncaRepositorioImpl(pisip_UrbanParkContext);
        }
        public async Task Info_EncaAddAsync(Informes_Encabezado TEntity)
        {
            await infoEncaRepositorio.AddAsync(TEntity);
        }
        public async Task Info_EncaUpdateAsync(Informes_Encabezado Entity)
        {
            await infoEncaRepositorio.UpdateAsync(Entity);
        }
        public async Task Info_EncaDeleteAsync(int id)
        {
            await infoEncaRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Informes_Encabezado>> Info_EncaGetAllAsync()
        {
            return await infoEncaRepositorio.GetAllAsync();
        }
        public async Task<Informes_Encabezado> Info_EncaGetByIdAsync(int id)
        {
            return await infoEncaRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Informes_Encabezado>> ListarInfo_EncaActivo()
        {
            return await infoEncaRepositorio.ListarInfo_EncaActivo();
        }

    }

}
