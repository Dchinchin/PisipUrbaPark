﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;


namespace UrbaPark.Dominio.Modelo.Entidades;

public partial class Usuarios
{
    public int IdUsuario { get; set; }
    public int? IdRol { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Cedula { get; set; }
    public string Contrasena { get; set; }
    public bool EstaEliminado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    public bool ContrasenaActualizada { get; set; }
    
    public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
    public virtual ICollection<Informes_Encabezado> InformesEncabezados { get; set; } = new List<Informes_Encabezado>();
    
    public virtual Roles IdRolNavigation { get; set; }
}