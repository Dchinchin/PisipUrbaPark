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
    public class CronogramaServicioImpl : ICronogramaServicio
    {
        private ICronogramaRepositorio cronogramaRepositorio;

        public CronogramaServicioImpl(Pisip_UrbanParkContext pisip_UrbanParkContext)
        {
            this.cronogramaRepositorio = new CronogramaRepositorioImpl(pisip_UrbanParkContext);
        }
        public async Task CronogramaAddAsync(Cronogramas TEntity)
        {
            await cronogramaRepositorio.AddAsync(TEntity);
        }
        public async Task CronogramaDeleteAsync(int id)
        {
            await cronogramaRepositorio.DeleteAsync(id);
        }
        public Task<IEnumerable<Cronogramas>> CronogramaGetAllAsync()
        {
            return cronogramaRepositorio.GetAllAsync();
        }
        public Task<Cronogramas> CronogramaGetByIdAsync(int id)
        {
            return cronogramaRepositorio.GetByIdAsync(id);
        }
        public async Task CronogramaUpdateAsync(Cronogramas Entity)
        {
            await cronogramaRepositorio.UpdateAsync(Entity);
        }
        public Task<List<Cronogramas>> ListarCronogramaActivo()
        {
            return cronogramaRepositorio.ListarCronogramaActivo();
        }


    }

    
}
