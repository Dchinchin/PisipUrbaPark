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
        Task CronogramaAddAsync(Bitacora TEntity); //insertar
        [OperationContract]
        Task CronogramaUpdateAsync(Bitacora Entity);//actualizar
        [OperationContract]
        Task CronogramaDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Bitacora>> CronogramaGetAllAsync();//listar todo
        [OperationContract]
        Task<Bitacora> CronogramaGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Cronogramas>> ListarCronograma(); //listar todo con Task<List<Bitacora>>

    }
}
