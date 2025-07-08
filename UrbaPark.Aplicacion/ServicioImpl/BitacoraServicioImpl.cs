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
    public class BitacoraServicioImpl : IBitacoraServicio
    {
        private readonly IBitacoraRepositorio bitacoraRepositorio;
        public BitacoraServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.bitacoraRepositorio = new BitacoraRepositorioImpl(pisip_UrbanParkContext);
        }
        public Task<List<Bitacora>> ListarBitacora()
        {
            return bitacoraRepositorio.ListarBitacora();
        }
        public async Task BitacoraAddAsync(Bitacora TEntity)
        {
            await bitacoraRepositorio.AddAsync(TEntity);
        }

        public async Task BitacoraDeleteAsync(int id)
        {
            await bitacoraRepositorio.DeleteAsync(id);
        }
        
  

        public Task<IEnumerable<Bitacora>> BitacoraGetAllAsync()
        {
            return bitacoraRepositorio.GetAllAsync();
        }

        public Task<Bitacora> BitacoraGetByIdAsync(int id)
        {
            return bitacoraRepositorio.GetByIdAsync(id);
        }

        public async Task BitacoraUpdateAsync(Bitacora Entity)
        {
            await bitacoraRepositorio.UpdateAsync(Entity);
        }
    }
}
