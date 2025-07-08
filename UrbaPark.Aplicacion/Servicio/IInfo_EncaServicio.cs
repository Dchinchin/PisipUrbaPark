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
    public interface IInfo_EncaServicio
    {
        [OperationContract]
        Task Info_EncaAddAsync(Informes_Encabezado TEntity); //insertar
        [OperationContract]
        Task Info_EncaUpdateAsync(Informes_Encabezado Entity);//actualizar
        [OperationContract]
        Task Info_EncaDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Informes_Encabezado>> Info_EncaGetAllAsync();//listar todo
        [OperationContract]
        Task<Informes_Encabezado> Info_EncaGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Informes_Encabezado>> ListarInfo_EncaActivo();

    }
}
