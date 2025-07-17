
#nullable disable
using System;
using System.Collections.Generic;

namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class Bitacora
{
    public int IdBitacora { get; set; }
    public int IdMantenimiento { get; set; }
    public DateTime FechaHora { get; set; }
    public string Descripcion { get; set; }
    public string ImagenUrl { get; set; }
    public bool EstaEliminado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    
    public virtual Mantenimiento IdMantenimientoNavigation { get; set; }
}