
using System;
using System.Collections.Generic;

namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class TipoMantenimiento
{
    public int IdTipo { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public bool EstaEliminado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
