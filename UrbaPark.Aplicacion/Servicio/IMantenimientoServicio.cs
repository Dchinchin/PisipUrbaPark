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
    public interface IMantenimientoServicio
    {
        [OperationContract]
        Task MantenimientoAddAsync(Mantenimientos TEntity); //insertar
        [OperationContract]
        Task MantenimientoUpdateAsync(Mantenimientos Entity);//actualizar
        [OperationContract]
        Task MantenimientoDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Mantenimientos>> MantenimientoGetAllAsync();//listar todo
        [OperationContract]
        Task<Mantenimientos> MantenimientoGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Mantenimientos>> ListarMantenimientoActivo();
    }
}
