namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class Mantenimiento
{
    public int IdMantenimiento { get; set; }
    public int? IdUsuario { get; set; }
    public int? IdParqueadero { get; set; }
    public int? IdTipoMantenimiento { get; set; }
    
    public int? IdInforme { get; set; }
    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Observaciones { get; set; }
    public string? Estado { get; set; }
    
    public bool? EstaEliminado { get; set; }
    
    public DateTime? FechaModificacion { get; set; }

    public virtual Usuarios? IdUsuarioNavigation { get; set; }
    public virtual Parqueadero? IdParqueaderoNavigation { get; set; }
    public virtual TipoMantenimiento? IdTipomantenimientoNavigation { get; set; }
    public virtual Informes_Encabezado? IdInformeNavigation { get; set; }
    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
}
