-- utilizar master antes de generar el script 
use master;
go

--creacion de base de datos Ibbcusca
create database MantenimientoGrupoGB;
go
-- empezamos a utilizar la base de datos creada
use MantenimientoGrupoGB;
go

-- TABLA USUARIO BASE DONDE SE ALOJNA LOS DATOS DEL USUARIO
CREATE TABLE [dbo].[UsuarioBase](
	[IdUsuario] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombres] [varchar](100) NOT NULL,
	[Apellidos] [varchar](100) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[DUI] [varchar](10) NOT NULL,
	[NIT] [nchar](17) NOT NULL,
	[ISSS] [varchar](9) NOT NULL,
	[Telefono] [varchar](14) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[EstadoEliminado] [bit] NOT NULL,
	[FechaEliminacion] [datetime] NULL
	)

