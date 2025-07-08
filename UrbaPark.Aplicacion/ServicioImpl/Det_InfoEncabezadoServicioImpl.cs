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
    public class Det_InfoEncabezadoServicioImpl : IDet_InfoEncabezadoServicio
    {
        private IDet_InfoEncabezadoRepositorio det_InfoEncabezadoRepositorio;

        public Det_InfoEncabezadoServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.det_InfoEncabezadoRepositorio = new Det_InfoEncabezadoRepositorioImpl(pisip_UrbanParkContext);

        }

        public async Task Det_InfoEncabezadoAddAsync(Detalle_Informe TEntity)
        {
            await det_InfoEncabezadoRepositorio.AddAsync(TEntity);
        }
        public async Task Det_InfoEncabezadoUpdateAsync(Detalle_Informe Entity)
        {
            await det_InfoEncabezadoRepositorio.UpdateAsync(Entity);
        }
        public async Task Det_InfoEncabezadoDeleteAsync(int id)
        {
            await det_InfoEncabezadoRepositorio.DeleteAsync(id);
        }
        public async Task<IEnumerable<Detalle_Informe>> Det_InfoEncabezadoGetAllAsync()
        {
            return await det_InfoEncabezadoRepositorio.GetAllAsync();
        }
        public async Task<Detalle_Informe> Det_InfoEncabezadoGetByIdAsync(int id)
        {
            return await det_InfoEncabezadoRepositorio.GetByIdAsync(id);
        }
        public async Task<List<Detalle_Informe>> ListarDet_InfoEncabezadoActivo()
        {
            return await det_InfoEncabezadoRepositorio.ListarDet_InfoEncabezadoActivo();
        }





    }
}
