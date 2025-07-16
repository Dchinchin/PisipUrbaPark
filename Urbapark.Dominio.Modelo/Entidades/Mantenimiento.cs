namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class Mantenimiento
{
    public int IdMantenimiento { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdParqueadero { get; set; }

    public int? IdTipomantenimiento { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaFin { get; set; }

    public string? Observaciones { get; set; }

    public virtual Parqueadero? IdParqueaderoNavigation { get; set; }

    public virtual TipoMantenimiento? IdTipomantenimientoNavigation { get; set; }

    public virtual Usuarios? IdUsuarioNavigation { get; set; }

    

    public virtual ICollection<Informes_Encabezado> Informes_Encabezados { get; set; } = new List<Informes_Encabezado>();

    public virtual ICollection<Bitacora> Bitacoras { get; set; } = new List<Bitacora>();
}
