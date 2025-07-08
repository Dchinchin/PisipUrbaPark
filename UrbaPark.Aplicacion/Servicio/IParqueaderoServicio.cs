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
    public interface IParqueaderoServicio
    {
        [OperationContract]
        Task ParqueaderoAddAsync(Parquadero TEntity); //insertar
        [OperationContract]
        Task ParqueaderoUpdateAsync(Parquadero Entity);//actualizar
        [OperationContract]
        Task ParqueaderoDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Parquadero>> ParqueaderoGetAllAsync();//listar todo
        [OperationContract]
        Task<Parquadero> ParqueaderoGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Parquadero>> ListarParqueaderoActivo();
    }
}
