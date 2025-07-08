using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UrbaPark.Infraestructura.AccesoDatos;

namespace UrbaPark.Aplicacion.Servicio
{
    [ServiceContract]
    public interface ICronogramaServicio
    {
        [OperationContract]
        Task CronogramaAddAsync(Cronogramas TEntity); //insertar
        [OperationContract]
        Task CronogramaUpdateAsync(Cronogramas Entity);//actualizar
        [OperationContract]
        Task CronogramaDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Cronogramas>> CronogramaGetAllAsync();//listar todo
        [OperationContract]
        Task<Cronogramas> CronogramaGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Cronogramas>> ListarCronogramaActivo();
    }
}
