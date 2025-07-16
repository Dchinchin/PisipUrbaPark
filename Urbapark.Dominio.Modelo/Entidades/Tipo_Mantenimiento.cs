
using System;
using System.Collections.Generic;

namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class TipoMantenimiento
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
}
