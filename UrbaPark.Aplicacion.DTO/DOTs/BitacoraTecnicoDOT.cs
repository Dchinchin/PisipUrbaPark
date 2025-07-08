using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbaPark.Aplicacion.DTO.DOTs
{
    public class BitacoraTecnicoDOT
    {
        public string DescripcionBitacora { get; set; }
        public int id_bitacora { get; set; }
        public List<string> NombresTecnicos { get; set; }
    }
}
