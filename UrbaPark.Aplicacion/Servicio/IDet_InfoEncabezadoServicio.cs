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
    public interface IDet_InfoEncabezadoServicio
    {
        [OperationContract]
        Task Det_InfoEncabezadoAddAsync(Detalle_Informe TEntity); //insertar
        [OperationContract]
        Task Det_InfoEncabezadoUpdateAsync(Detalle_Informe Entity);//actualizar
        [OperationContract]
        Task Det_InfoEncabezadoDeleteAsync(int id);//eliminar por ID
        [OperationContract]
        Task<IEnumerable<Detalle_Informe>> Det_InfoEncabezadoGetAllAsync();//listar todo
        [OperationContract]
        Task<Detalle_Informe> Det_InfoEncabezadoGetByIdAsync(int id);//buscar por ID
        [OperationContract]
        Task<List<Detalle_Informe>> ListarDet_InfoEncabezadoActivo();
    }
}
