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
    public interface IUsuariosServicio
    {
        [OperationContract]
        Task UsuariosAddAsync(Usuarios TEntity); //insertar
        [OperationContract]
        Task UsuariosUpdateAsync(Usuarios Entity);//actualizar
        [OperationContract]
        Task UsuariosDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Usuarios>> UsuariosGetAllAsync();//listar todo
        [OperationContract]
        Task<Usuarios> UsuariosGetByIdAsync(int id);//buscar por ID

        [OperationContract]
        Task<List<Usuarios>> ListarUsuariosActivo();
        
    }
}
