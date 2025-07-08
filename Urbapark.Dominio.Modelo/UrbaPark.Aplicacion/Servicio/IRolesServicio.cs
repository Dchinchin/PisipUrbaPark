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
    public interface IRolesServicio
    {
        [OperationContract]
        Task RolesAddAsync(Roles TEntity); //insertar
        [OperationContract]
        Task RolesUpdateAsync(Roles Entity);//actualizar
        [OperationContract]
        Task RolesDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Roles>> RolesGetAllAsync();//listar todo
        [OperationContract]
        Task<Roles> RolesGetByIdAsync(int id);//buscar por ID
    }
}
