using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
    public interface IBitacoraServicio
    {
        [OperationContract]
        Task BitacoraAddAsync(Bitacora TEntity); //insertar
        [OperationContract]
        Task BitacoraUpdateAsync(Bitacora Entity);//actualizar
        [OperationContract]
        Task BitacoraDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Bitacora>> BitacoraGetAllAsync();//listar todo
        [OperationContract]
        Task<Bitacora> BitacoraGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Bitacora>> ListarBitacoraActivo();

    }
}
