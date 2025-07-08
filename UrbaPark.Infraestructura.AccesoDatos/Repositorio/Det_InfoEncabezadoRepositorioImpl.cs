using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Dominio.Modelo.Abstracciones;

namespace UrbaPark.Infraestructura.AccesoDatos.Repositorio
{
    public class Det_InfoEncabezadoRepositorioImpl : RepositorioImpl<Informes_Encabezado>, IDet_InfoEncabezadoRepositorio   
    {
        private readonly Pisip_UrbanParkContext _UrbanParkContext;

        public Det_InfoEncabezadoRepositorioImpl(Pisip_UrbanParkContext dbContext) : base(dbContext)
        {
            _UrbanParkContext = dbContext;
        }

        public Task AddAsync(Detalle_Informe TEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Detalle_Informe>> ListarDet_InfoEncabezadoActivo()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Detalle_Informe Entity)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Detalle_Informe>> IRepositorio<Detalle_Informe>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Detalle_Informe> IRepositorio<Detalle_Informe>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
