USE [master]
GO
/****** Object:  Database [SIGMA]    Script Date: 6/3/2018 4:56:00 PM ******/
CREATE DATABASE [SIGMA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SIGMA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.VINCENTEX\MSSQL\DATA\SIGMA.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SIGMA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.VINCENTEX\MSSQL\DATA\SIGMA_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SIGMA] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIGMA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SIGMA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIGMA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIGMA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIGMA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIGMA] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIGMA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIGMA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SIGMA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIGMA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIGMA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIGMA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIGMA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIGMA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIGMA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIGMA] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SIGMA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIGMA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIGMA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIGMA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIGMA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIGMA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SIGMA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SIGMA] SET RECOVERY FULL 
GO
ALTER DATABASE [SIGMA] SET  MULTI_USER 
GO
ALTER DATABASE [SIGMA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIGMA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SIGMA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SIGMA] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SIGMA] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SIGMA]
GO
/****** Object:  Table [dbo].[ADMINISTRADOR]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADMINISTRADOR](
	[Id_Administrador] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usr_Admin] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Administrador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ADSCRIPTO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ADSCRIPTO](
	[Id_Adscripto] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usr_Adscripto] [int] NOT NULL,
	[Turno_Adscripto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Adscripto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ALUMNO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ALUMNO](
	[Id_Alumno] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usr_Alumno] [int] NOT NULL,
	[Grupo_Usr] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Alumno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BEACON]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BEACON](
	[Id_Beacon] [char](16) NOT NULL,
	[Id_Nodo_Beacon] [int] NOT NULL,
	[Status_Nodo] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Beacon] ASC,
	[Id_Nodo_Beacon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DOCENTE]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DOCENTE](
	[Id_Docente] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usr_Docente] [int] NOT NULL,
	[Materia_Docente] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Docente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ENCUESTA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ENCUESTA](
	[Id_Encuesta] [int] IDENTITY(1,1) NOT NULL,
	[Creador_Encuesta] [int] NOT NULL,
	[Fecha_Encuesta] [datetime] NOT NULL,
	[Fecha_Fin_Encuesta] [datetime] NOT NULL,
	[Titulo_Encuesta] [varchar](200) NOT NULL,
	[Descripcion_Encuesta] [varchar](600) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Encuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GRUPO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GRUPO](
	[Id_Grupo] [int] IDENTITY(1,1) NOT NULL,
	[Grado_Grupo] [int] NOT NULL,
	[Orientacion_Grupo] [int] NOT NULL,
	[Numero_Grupo] [int] NOT NULL,
	[Turno_Grupo] [int] NULL,
	[Anio_grupo] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Grupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HORA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HORA](
	[Id_Hora] [int] IDENTITY(1,1) NOT NULL,
	[Inicio_Hora] [time](7) NOT NULL,
	[Fin_Hora] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Hora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HORA_TURNO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HORA_TURNO](
	[Id_HT] [int] IDENTITY(1,1) NOT NULL,
	[Hora_HT] [int] NOT NULL,
	[Turno_HT] [int] NOT NULL,
	[Alias_HT] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_HT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HORARIO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HORARIO](
	[Id_Grupo_Horario] [int] NOT NULL,
	[Dia_Horario] [char](3) NOT NULL,
	[Hora_Horario] [int] NOT NULL,
	[Id_Materia_Horario] [int] NOT NULL,
	[Id_Docente_Horario] [int] NOT NULL,
	[Id_Salon] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Grupo_Horario] ASC,
	[Dia_Horario] ASC,
	[Hora_Horario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MATERIA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MATERIA](
	[Id_Materia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Materia] [varchar](80) NOT NULL,
	[Orientacion_Materia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Materia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NODO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NODO](
	[Id_Nodo] [int] IDENTITY(1,1) NOT NULL,
	[PosX_Nodo] [int] NOT NULL,
	[PosY_Nodo] [int] NOT NULL,
	[Piso_Nodo] [char](2) NOT NULL,
	[Tipo_Nodo] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Nodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NOTIFICACION]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NOTIFICACION](
	[Id_Notificacion] [int] IDENTITY(1,1) NOT NULL,
	[Destino_Notificacion] [char](1) NOT NULL,
	[Fecha_Notificacion] [datetime] NOT NULL,
	[Titulo_Notificacion] [varchar](80) NOT NULL,
	[Mensaje_Notificacion] [varchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Notificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NOTIFICACION_DESTINATARIO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NOTIFICACION_DESTINATARIO](
	[Id_ND] [int] NOT NULL,
	[Id_Destinatario] [int] NOT NULL,
	[Grupo_Actual_Usr] [int] NULL,
	[Fecha_Envio] [datetime] NOT NULL,
	[Fecha_Entrega] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_ND] ASC,
	[Id_Destinatario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OPCION_PREGUNTA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OPCION_PREGUNTA](
	[Id_OP] [int] IDENTITY(1,1) NOT NULL,
	[Id_Pregunta_OP] [int] NOT NULL,
	[Texto_OP] [varchar](600) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_OP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ORIENTACION]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ORIENTACION](
	[Id_Orientacion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Orientacion] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Orientacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PONDERACION]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PONDERACION](
	[Id_Nodo_Actual] [int] NOT NULL,
	[Id_Nodo_Adyacente] [int] NOT NULL,
	[Ponderacion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Nodo_Actual] ASC,
	[Id_Nodo_Adyacente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Posts]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [ntext] NULL,
	[BlogId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PREGUNTA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PREGUNTA](
	[Id_Pregunta] [int] IDENTITY(1,1) NOT NULL,
	[Id_Encuesta_Pregunta] [int] NOT NULL,
	[Texto_Pregunta] [varchar](600) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Pregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PRUEBA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PRUEBA](
	[Id_Prueba] [int] IDENTITY(1,1) NOT NULL,
	[Tipo_Prueba] [char](1) NOT NULL,
	[Id_Materia_Prueba] [int] NOT NULL,
	[Id_Grupo_Prueba] [int] NOT NULL,
	[Id_Docente_Prueba] [int] NOT NULL,
	[Fecha_Prueba] [date] NOT NULL,
	[Hora_Inicio_Prueba] [int] NOT NULL,
	[Hora_Fin_Prueba] [int] NOT NULL,
	[Temas_Prueba] [varchar](600) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Prueba] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RESPUESTA_ENCUESTA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RESPUESTA_ENCUESTA](
	[Id_Usuario_Encuestado] [int] NOT NULL,
	[Id_Pregunta_Respondida] [int] NOT NULL,
	[Id_Respuesta] [int] NOT NULL,
	[Id_Respusta_Encuesta] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_RESPUESTA_ENCUESTA] PRIMARY KEY CLUSTERED 
(
	[Id_Respusta_Encuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TAREA]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TAREA](
	[Id_Tarea] [int] IDENTITY(1,1) NOT NULL,
	[Id_Docente] [int] NOT NULL,
	[Id_Materia_Tarea] [int] NULL,
	[Contenido_Tarea] [varchar](600) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Tarea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TAREA_ADJUNTO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TAREA_ADJUNTO](
	[Id_Tarea_TA] [int] NULL,
	[Url_Adjunto] [varchar](300) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TAREA_GRUPO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAREA_GRUPO](
	[Id_TG] [int] NOT NULL,
	[Id_Grupo_TG] [int] NOT NULL,
	[Fecha_TG] [datetime] NOT NULL,
	[Fecha_Entrega_TG] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_TG] ASC,
	[Id_Grupo_TG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TURNO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TURNO](
	[Id_Turno] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Turno] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Turno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USUARIO](
	[Id_Usr] [int] IDENTITY(1,1) NOT NULL,
	[Cedula_Usr] [varchar](10) NOT NULL,
	[PassHash_Usr] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NULL,
	[Celular_Usr] [char](9) NULL,
	[Token_Usr] [varchar](50) NULL,
	[Nombre_Usr] [varchar](40) NULL,
	[Apellido_Usr] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Usr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Cedula_Usr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[NOTIFICACION] ADD  DEFAULT (getdate()) FOR [Fecha_Notificacion]
GO
ALTER TABLE [dbo].[NOTIFICACION_DESTINATARIO] ADD  DEFAULT (getdate()) FOR [Fecha_Envio]
GO
ALTER TABLE [dbo].[TAREA_GRUPO] ADD  DEFAULT (getdate()) FOR [Fecha_TG]
GO
ALTER TABLE [dbo].[ADMINISTRADOR]  WITH CHECK ADD FOREIGN KEY([Id_Usr_Admin])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[ADSCRIPTO]  WITH CHECK ADD FOREIGN KEY([Id_Usr_Adscripto])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[ADSCRIPTO]  WITH CHECK ADD FOREIGN KEY([Turno_Adscripto])
REFERENCES [dbo].[TURNO] ([Id_Turno])
GO
ALTER TABLE [dbo].[ALUMNO]  WITH CHECK ADD FOREIGN KEY([Grupo_Usr])
REFERENCES [dbo].[GRUPO] ([Id_Grupo])
GO
ALTER TABLE [dbo].[ALUMNO]  WITH CHECK ADD FOREIGN KEY([Id_Usr_Alumno])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[BEACON]  WITH CHECK ADD FOREIGN KEY([Id_Nodo_Beacon])
REFERENCES [dbo].[NODO] ([Id_Nodo])
GO
ALTER TABLE [dbo].[DOCENTE]  WITH CHECK ADD FOREIGN KEY([Id_Usr_Docente])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[DOCENTE]  WITH CHECK ADD FOREIGN KEY([Materia_Docente])
REFERENCES [dbo].[MATERIA] ([Id_Materia])
GO
ALTER TABLE [dbo].[ENCUESTA]  WITH CHECK ADD FOREIGN KEY([Creador_Encuesta])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[GRUPO]  WITH CHECK ADD FOREIGN KEY([Orientacion_Grupo])
REFERENCES [dbo].[ORIENTACION] ([Id_Orientacion])
GO
ALTER TABLE [dbo].[GRUPO]  WITH CHECK ADD FOREIGN KEY([Turno_Grupo])
REFERENCES [dbo].[TURNO] ([Id_Turno])
GO
ALTER TABLE [dbo].[HORA_TURNO]  WITH CHECK ADD FOREIGN KEY([Hora_HT])
REFERENCES [dbo].[HORA] ([Id_Hora])
GO
ALTER TABLE [dbo].[HORA_TURNO]  WITH CHECK ADD FOREIGN KEY([Turno_HT])
REFERENCES [dbo].[TURNO] ([Id_Turno])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([Hora_Horario])
REFERENCES [dbo].[HORA_TURNO] ([Id_HT])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([Id_Docente_Horario])
REFERENCES [dbo].[DOCENTE] ([Id_Docente])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([Id_Grupo_Horario])
REFERENCES [dbo].[GRUPO] ([Id_Grupo])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([Id_Materia_Horario])
REFERENCES [dbo].[MATERIA] ([Id_Materia])
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD FOREIGN KEY([Id_Salon])
REFERENCES [dbo].[NODO] ([Id_Nodo])
GO
ALTER TABLE [dbo].[MATERIA]  WITH CHECK ADD FOREIGN KEY([Orientacion_Materia])
REFERENCES [dbo].[ORIENTACION] ([Id_Orientacion])
GO
ALTER TABLE [dbo].[NOTIFICACION_DESTINATARIO]  WITH CHECK ADD FOREIGN KEY([Grupo_Actual_Usr])
REFERENCES [dbo].[GRUPO] ([Id_Grupo])
GO
ALTER TABLE [dbo].[NOTIFICACION_DESTINATARIO]  WITH CHECK ADD FOREIGN KEY([Id_Destinatario])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[NOTIFICACION_DESTINATARIO]  WITH CHECK ADD FOREIGN KEY([Id_ND])
REFERENCES [dbo].[NOTIFICACION] ([Id_Notificacion])
GO
ALTER TABLE [dbo].[OPCION_PREGUNTA]  WITH CHECK ADD FOREIGN KEY([Id_Pregunta_OP])
REFERENCES [dbo].[PREGUNTA] ([Id_Pregunta])
GO
ALTER TABLE [dbo].[PONDERACION]  WITH CHECK ADD FOREIGN KEY([Id_Nodo_Actual])
REFERENCES [dbo].[NODO] ([Id_Nodo])
GO
ALTER TABLE [dbo].[PONDERACION]  WITH CHECK ADD FOREIGN KEY([Id_Nodo_Adyacente])
REFERENCES [dbo].[NODO] ([Id_Nodo])
GO
ALTER TABLE [dbo].[PREGUNTA]  WITH CHECK ADD FOREIGN KEY([Id_Encuesta_Pregunta])
REFERENCES [dbo].[ENCUESTA] ([Id_Encuesta])
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD FOREIGN KEY([Hora_Fin_Prueba])
REFERENCES [dbo].[HORA_TURNO] ([Id_HT])
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD FOREIGN KEY([Hora_Inicio_Prueba])
REFERENCES [dbo].[HORA_TURNO] ([Id_HT])
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD FOREIGN KEY([Id_Docente_Prueba])
REFERENCES [dbo].[DOCENTE] ([Id_Docente])
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD FOREIGN KEY([Id_Grupo_Prueba])
REFERENCES [dbo].[GRUPO] ([Id_Grupo])
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD FOREIGN KEY([Id_Materia_Prueba])
REFERENCES [dbo].[MATERIA] ([Id_Materia])
GO
ALTER TABLE [dbo].[RESPUESTA_ENCUESTA]  WITH CHECK ADD FOREIGN KEY([Id_Pregunta_Respondida])
REFERENCES [dbo].[PREGUNTA] ([Id_Pregunta])
GO
ALTER TABLE [dbo].[RESPUESTA_ENCUESTA]  WITH CHECK ADD FOREIGN KEY([Id_Respuesta])
REFERENCES [dbo].[OPCION_PREGUNTA] ([Id_OP])
GO
ALTER TABLE [dbo].[RESPUESTA_ENCUESTA]  WITH CHECK ADD FOREIGN KEY([Id_Usuario_Encuestado])
REFERENCES [dbo].[USUARIO] ([Id_Usr])
GO
ALTER TABLE [dbo].[TAREA]  WITH CHECK ADD FOREIGN KEY([Id_Docente])
REFERENCES [dbo].[DOCENTE] ([Id_Docente])
GO
ALTER TABLE [dbo].[TAREA]  WITH CHECK ADD FOREIGN KEY([Id_Materia_Tarea])
REFERENCES [dbo].[MATERIA] ([Id_Materia])
GO
ALTER TABLE [dbo].[TAREA_ADJUNTO]  WITH CHECK ADD FOREIGN KEY([Id_Tarea_TA])
REFERENCES [dbo].[TAREA] ([Id_Tarea])
GO
ALTER TABLE [dbo].[TAREA_GRUPO]  WITH CHECK ADD FOREIGN KEY([Id_Grupo_TG])
REFERENCES [dbo].[GRUPO] ([Id_Grupo])
GO
ALTER TABLE [dbo].[TAREA_GRUPO]  WITH CHECK ADD FOREIGN KEY([Id_TG])
REFERENCES [dbo].[TAREA] ([Id_Tarea])
GO
ALTER TABLE [dbo].[BEACON]  WITH CHECK ADD CHECK  (([Status_Nodo]='I' OR [Status_Nodo]='A'))
GO
ALTER TABLE [dbo].[HORARIO]  WITH CHECK ADD CHECK  (([Dia_Horario]='VIE' OR [Dia_Horario]='JUE' OR [Dia_Horario]='MIE' OR [Dia_Horario]='MAR' OR [Dia_Horario]='LUN'))
GO
ALTER TABLE [dbo].[NODO]  WITH CHECK ADD CHECK  (([Piso_Nodo]='02' OR [Piso_Nodo]='01' OR [Piso_Nodo]='PB' OR [Piso_Nodo]='SS'))
GO
ALTER TABLE [dbo].[NOTIFICACION]  WITH CHECK ADD CHECK  (([Destino_Notificacion]='T' OR [Destino_Notificacion]='G' OR [Destino_Notificacion]='I'))
GO
ALTER TABLE [dbo].[PRUEBA]  WITH CHECK ADD CHECK  (([Tipo_prueba]='P' OR [Tipo_prueba]='E'))
GO
/****** Object:  StoredProcedure [dbo].[PR_Ingresar_Orientacion]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------
-------------------------------------    STORE PROCEDURES     ----------------------------------------
------------------------------------------------------------------------------------------------------

--Ingreso de un Usuario

CREATE PROCEDURE [dbo].[PR_Ingresar_Orientacion]
    @Nombre_Orientacion varchar(30),
	@mensajeRespuesta int output
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO dbo.[Orientacion] (Nombre_Orientacion)
        VALUES(@Nombre_Orientacion)

       SET @mensajeRespuesta=1
    END TRY
    BEGIN CATCH
        SET @mensajeRespuesta=-1
    END CATCH
END

exec dbo.PR_Ingresar_Orientacion "Cientifico"
GO
/****** Object:  StoredProcedure [dbo].[PR_Ingresar_Usuario]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------
-------------------------------------    STORE PROCEDURES     ----------------------------------------
------------------------------------------------------------------------------------------------------

--Ingreso de un Usuario

create PROCEDURE [dbo].[PR_Ingresar_Usuario]
    @pCedula_Usr varchar(10), 
    @pPassword varchar(50),
	@pCelular_Usr char(9),
    @pToken_Usr varchar(50), 
    @pNombre_Usr varchar(40),
	@pApellido_Usr varchar(40),
    @mensajeRespuesta varchar(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[USUARIO] (Cedula_Usr, PassHash_Usr, Salt, Celular_Usr, Token_Usr, Nombre_Usr, Apellido_Usr)
        VALUES(@pCedula_Usr, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt, @pCelular_Usr, @pToken_Usr,@pNombre_Usr,@pApellido_Usr)

       SET @mensajeRespuesta='INGRESO_USUARIO_OK'

    END TRY
    BEGIN CATCH
        SET @mensajeRespuesta='INGRESO_USUARIO_ERROR' 
    END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[PR_Login]    Script Date: 6/3/2018 4:56:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--PRUEBA
/*
DECLARE @respuesta VARCHAR(250)
EXEC dbo.PR_Ingresar_Usuario
    @pCedula_Usr ='12345678', 
    @pPassword ='1234',
	@pCelular_Usr ='099xxxxxx',
    @pToken_Usr ='rrrrrrrrrrrrr', 
    @pNombre_Usr ='Juan',
	@pApellido_Usr= 'Pérez',
    @mensajeRespuesta=@respuesta OUTPUT
	SELECT	@respuesta as N'@mensajeRespuesta'
select * from USUARIO
*/


-- Login

CREATE PROCEDURE [dbo].[PR_Login]
    @pCedula varchar(10), 
    @pPassword varchar(50),
    @mensajeRespuesta varchar(250)='' OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @Id_Usr INT

    IF EXISTS (SELECT TOP 1 Id_Usr FROM [dbo].[USUARIO] WHERE Cedula_Usr=@pCedula)
    BEGIN
        SET @Id_Usr=(SELECT Id_Usr FROM [dbo].[USUARIO] WHERE Cedula_Usr=@pCedula AND PassHash_Usr=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

       IF(@Id_Usr IS NULL)
           SET @mensajeRespuesta='ERROR'
       ELSE 
           SET @mensajeRespuesta='OK'
    END
    ELSE
       SET @mensajeRespuesta='ERROR'

END

GO
USE [master]
GO
ALTER DATABASE [SIGMA] SET  READ_WRITE 
GO
