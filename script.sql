USE [master]
GO
/****** Object:  Database [Pisip_UrbanPark]    Script Date: 24/7/2025 6:08:13 ******/
CREATE DATABASE [Pisip_UrbanPark]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Pisip_UrbanPark', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Pisip_UrbanPark.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Pisip_UrbanPark_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Pisip_UrbanPark_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Pisip_UrbanPark] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pisip_UrbanPark].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pisip_UrbanPark] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pisip_UrbanPark] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pisip_UrbanPark] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Pisip_UrbanPark] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pisip_UrbanPark] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET RECOVERY FULL 
GO
ALTER DATABASE [Pisip_UrbanPark] SET  MULTI_USER 
GO
ALTER DATABASE [Pisip_UrbanPark] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pisip_UrbanPark] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pisip_UrbanPark] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pisip_UrbanPark] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Pisip_UrbanPark] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Pisip_UrbanPark] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Pisip_UrbanPark', N'ON'
GO
ALTER DATABASE [Pisip_UrbanPark] SET QUERY_STORE = ON
GO
ALTER DATABASE [Pisip_UrbanPark] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Pisip_UrbanPark]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id_bitacora] [int] IDENTITY(1,1) NOT NULL,
	[id_mantenimiento] [int] NULL,
	[fecha_hora] [datetime] NULL,
	[descripcion] [varchar](1000) NULL,
	[imagen_url] [varchar](255) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_bitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detalle_Informe]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalle_Informe](
	[id_detInfo] [int] IDENTITY(1,1) NOT NULL,
	[id_informe] [int] NULL,
	[descripcion] [varchar](1000) NULL,
	[archivo_url] [varchar](255) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_detInfo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Informes_Encabezado]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Informes_Encabezado](
	[id_informe] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](100) NULL,
	[id_usuario] [int] NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_informe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mantenimiento]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mantenimiento](
	[id_mantenimiento] [int] IDENTITY(1,1) NOT NULL,
	[id_usuario] [int] NULL,
	[id_parqueadero] [int] NULL,
	[id_tipomantenimiento] [int] NULL,
	[id_informe] [int] NULL,
	[fecha_inicio] [datetime] NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_fin] [datetime] NULL,
	[observaciones] [varchar](500) NULL,
	[estado] [varchar](50) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_mantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parqueadero]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parqueadero](
	[id_parqueadero] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](80) NULL,
	[direccion] [varchar](80) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_parqueadero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[nombre_rol] [varchar](80) NULL,
	[descripcion] [varchar](80) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Mantenimiento]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Mantenimiento](
	[id_tipomantenimiento] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](80) NULL,
	[descripcion] [varchar](80) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipomantenimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 24/7/2025 6:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_rol] [int] NULL,
	[nombre] [varchar](80) NULL,
	[apellido] [varchar](80) NULL,
	[correo] [varchar](80) NULL,
	[contrasena] [varchar](500) NULL,
	[cedula] [varchar](10) NULL,
	[esta_eliminado] [bit] NOT NULL,
	[fecha_creacion] [datetime] NULL,
	[fecha_modificacion] [datetime] NULL,
	[contrasena_actualizada] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora] ADD  DEFAULT (getdate()) FOR [fecha_hora]
GO
ALTER TABLE [dbo].[Bitacora] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Bitacora] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Detalle_Informe] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Detalle_Informe] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Informes_Encabezado] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Informes_Encabezado] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Mantenimiento] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Mantenimiento] ADD  DEFAULT ('Pendiente') FOR [estado]
GO
ALTER TABLE [dbo].[Mantenimiento] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Parqueadero] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Parqueadero] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Tipo_Mantenimiento] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Tipo_Mantenimiento] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [esta_eliminado]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [fecha_creacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [contrasena_actualizada]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Mantenimiento] FOREIGN KEY([id_mantenimiento])
REFERENCES [dbo].[Mantenimiento] ([id_mantenimiento])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Mantenimiento]
GO
ALTER TABLE [dbo].[Detalle_Informe]  WITH CHECK ADD  CONSTRAINT [FK_Detalle_Informe] FOREIGN KEY([id_informe])
REFERENCES [dbo].[Informes_Encabezado] ([id_informe])
GO
ALTER TABLE [dbo].[Detalle_Informe] CHECK CONSTRAINT [FK_Detalle_Informe]
GO
ALTER TABLE [dbo].[Informes_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Informes_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Informes_Encabezado] CHECK CONSTRAINT [FK_Informes_Usuario]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Informe] FOREIGN KEY([id_informe])
REFERENCES [dbo].[Informes_Encabezado] ([id_informe])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Informe]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Parqueadero] FOREIGN KEY([id_parqueadero])
REFERENCES [dbo].[Parqueadero] ([id_parqueadero])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Parqueadero]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Tipo] FOREIGN KEY([id_tipomantenimiento])
REFERENCES [dbo].[Tipo_Mantenimiento] ([id_tipomantenimiento])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Tipo]
GO
ALTER TABLE [dbo].[Mantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_Mantenimiento_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[Mantenimiento] CHECK CONSTRAINT [FK_Mantenimiento_Usuario]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([id_rol])
REFERENCES [dbo].[Roles] ([id_rol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
USE [master]
GO
ALTER DATABASE [Pisip_UrbanPark] SET  READ_WRITE 
GO
