#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UrbaPark.Dominio.Modelo.Entidades;

namespace UrbaPark.Infraestructura.AccesoDatos;

public partial class Pisip_UrbanParkContext : DbContext
{
    public Pisip_UrbanParkContext()
    {
    }

    public Pisip_UrbanParkContext(DbContextOptions<Pisip_UrbanParkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacora { get; set; }
    public virtual DbSet<Detalle_Informe> Detalle_Informe { get; set; }
    public virtual DbSet<Informes_Encabezado> Informes_Encabezado { get; set; }
    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }
    public virtual DbSet<Parqueadero> Parqueadero { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
    public virtual DbSet<TipoMantenimiento> TipoMantenimiento { get; set; }
    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora).HasName("PK__Bitacora__7E4268B0866136D2");

            entity.Property(e => e.IdBitacora).HasColumnName("id_bitacora");
            entity.Property(e => e.IdMantenimiento).HasColumnName("id_mantenimiento");
            entity.Property(e => e.FechaHora).HasColumnName("fecha_hora").HasColumnType("datetime");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.ImagenUrl).HasColumnName("imagen_url").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.IdMantenimientoNavigation).WithMany(p => p.Bitacoras)
                .HasForeignKey(d => d.IdMantenimiento)
                .HasConstraintName("FK_Bitacora_Mantenimiento");
        });

        modelBuilder.Entity<Detalle_Informe>(entity =>
        {
            entity.HasKey(e => e.IdDetInfo).HasName("PK__Detalle___888420765569AF1A");

            entity.Property(e => e.IdDetInfo).HasColumnName("id_detInfo");
            entity.Property(e => e.IdInforme).HasColumnName("id_informe");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.ArchivoUrl).HasColumnName("archivo_url").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.IdInformeNavigation).WithMany(p => p.Detalle_Informe)
                .HasForeignKey(d => d.IdInforme)
                .HasConstraintName("FK_Detalle_Informe_Informes_Encabezado");
        });

        modelBuilder.Entity<Informes_Encabezado>(entity =>
        {
            entity.HasKey(e => e.IdInforme).HasName("PK__Informes__ECCCE10F31757FB6");
            
            entity.Property(e => e.IdInforme).HasColumnName("id_informe");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Titulo).HasColumnName("titulo").HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.InformesEncabezados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Informes_Encabezado_Usuario");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimiento).HasName("PK__Mantenim__BEB925820A59D82B");

            entity.ToTable("Mantenimiento");

            entity.Property(e => e.IdMantenimiento).HasColumnName("id_mantenimiento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdParqueadero).HasColumnName("id_parqueadero");
            entity.Property(e => e.IdTipoMantenimiento).HasColumnName("id_tipomantenimiento");
            entity.Property(e => e.IdInforme).HasColumnName("id_informe");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio").HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin").HasColumnType("datetime");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.IdParqueaderoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdParqueadero)
                .HasConstraintName("FK_Mantenimiento_Parqueadero");

            entity.HasOne(d => d.IdTipomantenimientoNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdTipoMantenimiento)
                .HasConstraintName("FK_Mantenimiento_TipoMantenimiento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Mantenimiento_Usuario");
                
            entity.HasOne(d => d.IdInformeNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdInforme)
                .HasConstraintName("FK_Mantenimiento_Informe");
        });
        
        modelBuilder.Entity<TipoMantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__Tipo_Man__3213E83FFE8A8B4F");
            
            entity.ToTable("Tipo_Mantenimiento");
            
            entity.Property(e => e.IdTipo).HasColumnName("id_tipomantenimiento");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(1000).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");
        });

        modelBuilder.Entity<Parqueadero>(entity =>
        {
            entity.HasKey(e => e.IdParqueadero).HasName("PK__Parquade__0A2A3B21893B63E6");

            entity.Property(e => e.IdParqueadero).HasColumnName("id_parqueadero");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Direccion).HasColumnName("direccion").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__6ABCB5E0745D9BAC");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.NombreRol).HasColumnName("nombre_rol").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04AD51CD0BF7");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Apellido).HasColumnName("apellido").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Correo).HasColumnName("correo").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.Cedula).HasColumnName("cedula").HasMaxLength(10).IsUnicode(false);
            entity.Property(e => e.Contrasena).HasColumnName("contrasena").HasMaxLength(80).IsUnicode(false);
            entity.Property(e => e.EstaEliminado).HasColumnName("esta_eliminado");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}