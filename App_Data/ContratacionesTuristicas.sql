USE [master]
GO
/****** Object:  Database [ContratacionesTuristicas]    Script Date: 29/07/2016 15:08:30 ******/
CREATE DATABASE [ContratacionesTuristicas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ContratacionesTuristicas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ContratacionesTuristicas.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ContratacionesTuristicas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ContratacionesTuristicas_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ContratacionesTuristicas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ContratacionesTuristicas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ContratacionesTuristicas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET ARITHABORT OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ContratacionesTuristicas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ContratacionesTuristicas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ContratacionesTuristicas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ContratacionesTuristicas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ContratacionesTuristicas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET RECOVERY FULL 
GO
ALTER DATABASE [ContratacionesTuristicas] SET  MULTI_USER 
GO
ALTER DATABASE [ContratacionesTuristicas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ContratacionesTuristicas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ContratacionesTuristicas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ContratacionesTuristicas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ContratacionesTuristicas', N'ON'
GO
USE [ContratacionesTuristicas]
GO
/****** Object:  Table [dbo].[tbl_acciones]    Script Date: 29/07/2016 15:08:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_acciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_acciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_aerolineas]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_aerolineas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[razon_social] [varchar](max) NULL,
	[identificador] [varchar](15) NULL,
	[direccion] [varchar](max) NULL,
	[telefono_fijo] [varchar](13) NULL,
	[telefono_movil] [varchar](13) NULL,
	[email] [varchar](150) NULL,
	[web] [varchar](150) NULL,
	[codigo] [varchar](10) NULL,
	[IATA] [varchar](3) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[fecha_anulacion] [date] NULL,
	[id_usuario_anulacion] [int] NULL,
 CONSTRAINT [PK_tbl_aerolineas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_aerolineas_contactos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_aerolineas_contactos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_aerolinea] [int] NULL,
	[nombre] [varchar](250) NULL,
	[cargo] [varchar](250) NULL,
	[telefono] [varchar](13) NULL,
 CONSTRAINT [PK_tbl_aerolineas_contacto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_agencias]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_agencias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[razon_social] [varchar](max) NULL,
	[rif] [varchar](10) NULL,
	[direccion] [varchar](max) NULL,
	[telefono_fijo] [varchar](13) NULL,
	[telefono_movil] [varchar](13) NULL,
	[email] [varchar](150) NULL,
	[web] [varchar](150) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[fecha_anulacion] [date] NULL,
	[id_usuario_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_agencias] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_agencias_contactos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_agencias_contactos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_agencia] [int] NOT NULL,
	[nombre] [varchar](250) NULL,
	[cargo] [varchar](50) NULL,
	[telefono] [varchar](13) NULL,
 CONSTRAINT [PK_tbl_agencias_contacto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_bancos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_bancos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_bancos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_cargas]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_cargas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NULL,
	[tipo] [int] NULL,
	[registros_procesados] [int] NULL,
	[registros_fallidos] [int] NULL,
	[id_usuario] [int] NULL,
	[observacion] [varchar](max) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
 CONSTRAINT [PK_tbl_cargas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_destinos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_destinos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_destinos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_formas_pagos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_formas_pagos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
 CONSTRAINT [PK_tbl_formas_pagos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_freelances]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_freelances](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[rif] [varchar](10) NOT NULL,
	[direccion] [varchar](max) NULL,
	[telefono_fijo] [varchar](13) NULL,
	[telefono_movil] [varchar](13) NULL,
	[email] [varchar](150) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [date] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_freelances] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_hoteles]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_hoteles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[identificador] [varchar](15) NULL,
	[direccion] [varchar](max) NULL,
	[telefono_fijo] [varchar](13) NULL,
	[telefono_movil] [varchar](13) NULL,
	[email] [varchar](150) NULL,
	[codigo] [int] NULL,
	[razon_social] [varchar](max) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_hoteles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_hoteles_contactos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_hoteles_contactos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_hotel] [int] NULL,
	[nombre] [varchar](250) NULL,
	[cargo] [varchar](250) NULL,
	[telefono] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_hoteles_contactos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_modulos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_modulos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_padre] [int] NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[posicion] [int] NOT NULL,
	[url] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_items] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_operaciones]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_operaciones](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_modulo] [int] NOT NULL,
	[id_accion] [int] NOT NULL,
	[comentario] [varchar](max) NOT NULL,
	[resultado] [bit] NOT NULL,
	[id_usuario_registro] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_paquetes]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_paquetes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[fecha_inicio] [date] NULL,
	[fecha_fin] [date] NULL,
	[id_destino] [int] NULL,
	[tipo] [bit] NULL,
	[grupo] [bit] NULL,
	[activo] [bit] NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_paquetes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_pasajeros]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_pasajeros](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[rif] [varchar](10) NULL,
	[telefono] [varchar](12) NULL,
	[email] [varchar](50) NULL,
	[tipo] [tinyint] NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_pasajeros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_permisos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_permisos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_modulo] [int] NOT NULL,
	[id_accion] [int] NOT NULL,
	[id_rol] [int] NOT NULL,
	[permiso] [bit] NOT NULL,
	[id_usuario_registro] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[id_usuario_ult] [int] NOT NULL,
	[fecha_ult] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_permisos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_recibos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_recibos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](10) NULL,
	[fecha] [date] NULL,
	[monto] [float] NULL,
	[id_venta] [int] NULL,
	[concepto] [varchar](max) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_recibos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_recibos_formasPagos]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_recibos_formasPagos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_recibo] [int] NULL,
	[monto] [float] NULL,
	[banco] [int] NULL,
	[fecha] [date] NULL,
	[id_formaPago] [int] NULL,
 CONSTRAINT [PK_tbl_recibos_formasPagos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_roles]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](255) NULL,
	[id_usuario_registro] [int] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[id_usuario_ult] [int] NOT NULL,
	[fecha_ult] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_sucursales]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_sucursales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[codigo] [varchar](10) NULL,
	[rif] [varchar](10) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_sucursales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_usuarios]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_rol] [smallint] NULL,
	[id_cliente] [int] NULL,
	[nombre] [varchar](100) NULL,
	[apellido] [varchar](100) NULL,
	[usuario] [varchar](50) NULL,
	[clave] [varchar](max) NULL,
	[email] [varchar](255) NULL,
	[telefono] [varchar](50) NULL,
	[activo] [bit] NULL,
	[cambiar_clave] [bit] NULL,
	[id_usuario_reg] [int] NOT NULL,
	[fecha_reg] [datetime] NOT NULL,
	[id_usuario_ult] [int] NOT NULL,
	[fecha_ult] [datetime] NOT NULL,
 CONSTRAINT [PK_tbl_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_ventas]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_ventas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](10) NULL,
	[fecha] [date] NULL,
	[tipo] [int] NULL,
	[id_agencia] [int] NULL,
	[id_freelance] [int] NULL,
	[id_vendedor] [int] NULL,
	[total] [float] NULL,
	[tax] [float] NULL,
	[porcentaje_comision] [float] NULL,
	[comision_ct] [float] NULL,
	[porcentaje_ctg] [float] NULL,
	[fee] [float] NULL,
	[impuesto_fee] [float] NULL,
	[tota_reporte] [float] NULL,
	[monto_pagar] [float] NULL,
	[id_cliente] [int] NULL,
	[nombre_clente] [varchar](200) NULL,
	[identificacion_cliente] [varchar](12) NULL,
	[numero_boleto] [varchar](10) NULL,
	[descripcion_boleto] [varchar](10) NULL,
	[id_aerolinea] [int] NULL,
	[id_sistema] [int] NULL,
	[pantalla] [varchar](8) NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_ventas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_voucher_detalle]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_voucher_detalle](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_voucher] [int] NULL,
	[id_venta] [int] NULL,
	[nombre_pasajero] [varchar](200) NULL,
 CONSTRAINT [PK_tbl_voucher_detalle] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_vouchers]    Script Date: 29/07/2016 15:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_vouchers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NULL,
	[id_hotel] [int] NULL,
	[id_aerolinea] [int] NULL,
	[reserva] [varchar](12) NULL,
	[fecha_entrada] [date] NULL,
	[fecha_salida] [date] NULL,
	[tipo_habitacion] [int] NULL,
	[desayuno] [bit] NULL,
	[todo_incluido] [bit] NULL,
	[hospedaje] [bit] NULL,
	[id_usuario_reg] [int] NULL,
	[fecha_reg] [date] NULL,
	[anulado] [bit] NULL,
	[id_usuario_anulacion] [int] NULL,
	[fecha_anulacion] [date] NULL,
 CONSTRAINT [PK_tbl_vouchers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbl_modulos] ADD  CONSTRAINT [DF_tbl_items_id_padre]  DEFAULT ((0)) FOR [id_padre]
GO
ALTER TABLE [dbo].[tbl_modulos] ADD  CONSTRAINT [DF_tbl_items_posicion]  DEFAULT ((0)) FOR [posicion]
GO
ALTER TABLE [dbo].[tbl_operaciones] ADD  CONSTRAINT [DF_tbl_logs_id_item]  DEFAULT ((0)) FOR [id_modulo]
GO
ALTER TABLE [dbo].[tbl_operaciones] ADD  CONSTRAINT [DF_tbl_logs_id_accion]  DEFAULT ((0)) FOR [id_accion]
GO
ALTER TABLE [dbo].[tbl_operaciones] ADD  CONSTRAINT [DF_tbl_logs_id_usuario_registro]  DEFAULT ((0)) FOR [id_usuario_registro]
GO
ALTER TABLE [dbo].[tbl_operaciones] ADD  CONSTRAINT [DF_tbl_logs_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_id_item]  DEFAULT ((0)) FOR [id_modulo]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_id_accion]  DEFAULT ((0)) FOR [id_accion]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_id_rol]  DEFAULT ((0)) FOR [id_rol]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_permiso]  DEFAULT ((1)) FOR [permiso]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_id_usuario_registro]  DEFAULT ((0)) FOR [id_usuario_registro]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_id_usuario_ult]  DEFAULT ((0)) FOR [id_usuario_ult]
GO
ALTER TABLE [dbo].[tbl_permisos] ADD  CONSTRAINT [DF_tbl_permisos_fecha_ult]  DEFAULT (getdate()) FOR [fecha_ult]
GO
ALTER TABLE [dbo].[tbl_roles] ADD  CONSTRAINT [DF_tbl_roles_id_usuario_registro]  DEFAULT ((1)) FOR [id_usuario_registro]
GO
ALTER TABLE [dbo].[tbl_roles] ADD  CONSTRAINT [DF_tbl_roles_fecha_registro]  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[tbl_roles] ADD  CONSTRAINT [DF_tbl_roles_id_usuario_ult]  DEFAULT ((1)) FOR [id_usuario_ult]
GO
ALTER TABLE [dbo].[tbl_roles] ADD  CONSTRAINT [DF_tbl_roles_fecha_ult]  DEFAULT (getdate()) FOR [fecha_ult]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'para indicar si el pasajero es de tercera edad, adulto, ninho o infante. 4,3,2,1 respectivamente' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_pasajeros', @level2type=N'COLUMN',@level2name=N'tipo'
GO
USE [master]
GO
ALTER DATABASE [ContratacionesTuristicas] SET  READ_WRITE 
GO
