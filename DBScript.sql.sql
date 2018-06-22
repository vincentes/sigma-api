/*
create database SIGMA;
GO
use SIGMA;
GO
*/
------------------------------------------------------------------------------------------------------
-------------------------------------         TABLAS          ----------------------------------------
------------------------------------------------------------------------------------------------------
create table ORIENTACION (

Id_Orientacion int identity primary key not null,
Nombre_Orientacion varchar(30) not null
--Para cuarto año se crea "no aplica"
);
GO
create table TURNO (
Id_Turno int identity primary key not null,
Nombre_Turno varchar(20) not null
);
GO
create table HORA (
Id_Hora int identity primary key not null,
Inicio_Hora time not null,
Fin_Hora time not null,
);
GO
create table HORA_TURNO (
Id_HT int identity primary key not null,
Hora_HT int not null,
Turno_HT int not null,
Alias_HT varchar(20) not null,
 --primary key (Hora_HT, Turno_HT),
 foreign key (Hora_HT) references HORA(Id_Hora),
 foreign key (Turno_HT) references TURNO(Id_Turno),
);
GO
create table MATERIA (

Id_Materia int identity primary key not null,
Nombre_Materia varchar(80) not null,
Orientacion_Materia int not null,
foreign key(Orientacion_Materia) references ORIENTACION(Id_Orientacion)
);
GO

create table GRUPO (

Id_Grupo int identity primary key not null,
Grado_Grupo int not null,
Orientacion_Grupo int not null,
Numero_Grupo int not null,
Turno_Grupo int,
Anio_grupo int not null,
foreign key(Orientacion_Grupo) references ORIENTACION(Id_Orientacion),
foreign key(Turno_Grupo) references TURNO(Id_Turno)
);
GO

create table USUARIO (

Id_Usr int identity primary key not null,
Cedula_Usr varchar(10) unique not null,
PassHash_Usr BINARY(64) not null,
Salt UNIQUEIDENTIFIER,
Celular_Usr char(9),
--Falta definir el formato exácto del token
Token_Usr varchar(50),
Nombre_Usr varchar(40),
Apellido_Usr varchar(40),
);

GO

create table ALUMNO (
Id_Alumno nvarchar(250) primary key not null,
Id_Usr_Alumno nvarchar(450) not null,
Grupo_Usr int not null,
foreign key(Id_Usr_Alumno) references AspNetUsers(Id),
foreign key(Grupo_Usr) references GRUPO(Id_Grupo)
);
GO
create table DOCENTE (
Id_Docente int identity primary key not null,
Id_Usr_Docente nvarchar(450) not null,
Materia_Docente int not null,
foreign key(Id_Usr_Docente) references AspNetUsers(Id),
foreign key(Materia_Docente) references MATERIA(Id_Materia)
);
GO
create table ADSCRIPTO (
Id_Adscripto int identity primary key not null,
Id_Usr_Adscripto nvarchar(450) not null,
Turno_Adscripto int not null,
foreign key(Id_Usr_Adscripto) references AspNetUsers(Id),
foreign key(Turno_Adscripto) references TURNO(Id_Turno)
);
GO
create table ADMINISTRADOR (
Id_Administrador int identity primary key not null,
Id_Usr_Admin nvarchar(450) not null,
foreign key(Id_Usr_Admin) references AspNetUsers(Id),
);
GO

create table NODO (

Id_Nodo int identity primary key not null,
PosX_Nodo int not null,
PosY_Nodo int not null,
Piso_Nodo char(2) not null check (Piso_Nodo in ('SS', 'PB', '01', '02')),
--"Ss" SubSuelo, "PB" Planta Baja
Tipo_Nodo varchar(30) not null
--"Salón", "Laboratorio", etc.
);
GO
create table BEACON (

 Id_Beacon char(16),
 Id_Nodo_Beacon int,
 Status_Nodo char(1) not null check (Status_Nodo in ('A', 'I'))
 -- "A" Activo, "I" Inactivo
 primary key (Id_Beacon, Id_Nodo_Beacon),
 foreign key (Id_Nodo_Beacon) references NODO(Id_Nodo)
 );
 GO
 create table PONDERACION (

 Id_Nodo_Actual int,
 Id_Nodo_Adyacente int not null,
 Ponderacion int not null,
 primary key (Id_Nodo_Actual, Id_Nodo_Adyacente),
 foreign key (Id_Nodo_Actual) references NODO(Id_Nodo),
 foreign key (Id_Nodo_Adyacente) references NODO(Id_Nodo)
 );
 GO
 create table HORARIO (

Id_Grupo_Horario int not null,
Dia_Horario char(3) not null check (Dia_Horario in ('LUN', 'MAR', 'MIE', 'JUE', 'VIE')),
Hora_Horario int not null,
Id_Materia_Horario int not null,
Id_Docente_Horario int not null,
Id_Salon int not null,
primary key (Id_Grupo_Horario, Dia_Horario, Hora_Horario),
foreign key(Id_Grupo_Horario) references GRUPO(Id_Grupo),
foreign key(Hora_Horario) references HORA_TURNO(Id_HT),
foreign key(Id_Materia_Horario) references MATERIA(Id_Materia),
foreign key(Id_Docente_Horario) references DOCENTE(Id_Docente),
foreign key(Id_Salon) references NODO(Id_Nodo)
);
GO
 create table NOTIFICACION (

 Id_Notificacion int identity primary key not null,
 Destino_Notificacion char(1) not null check (Destino_Notificacion in ('I', 'G', 'T')),
 --"I" Individual, "G" Grupo, "T" Todos
 Fecha_Notificacion Datetime Default GetDate() not null,
 Titulo_Notificacion varchar(80) not null,
 Mensaje_Notificacion varchar(256) not null
 --256 Por limitación de las push notification
 );
 GO
 create table NOTIFICACION_DESTINATARIO (

 Id_ND int not null,
 Id_Destinatario int not null,
 Grupo_Actual_Usr int,
 Fecha_Envio Datetime Default GetDate() not null,
 Fecha_Entrega Datetime,
 primary key (Id_ND, Id_Destinatario),
 foreign key (Id_ND) references NOTIFICACION(Id_Notificacion),
 foreign key (Id_Destinatario) references USUARIO(Id_Usr),
 foreign key (Grupo_Actual_Usr) references GRUPO(Id_Grupo)
 );
 GO
  create table TAREA (

 Id_Tarea int identity primary key not null,
 Id_Docente int not null,
 Id_Materia_Tarea int,
 Contenido_Tarea varchar(600) not null,
 foreign key (Id_Docente) references DOCENTE(Id_Docente),
 foreign key (Id_Materia_Tarea) references MATERIA(Id_Materia)
 );
 GO
 create table TAREA_GRUPO (

 Id_TG int,
 Id_Grupo_TG int,
 Fecha_TG Datetime Default GetDate() not null,
 Fecha_Entrega_TG Datetime not null,
 primary key (Id_TG, Id_Grupo_TG),
 foreign key (Id_TG) references TAREA(Id_Tarea),
 foreign key (Id_Grupo_TG) references GRUPO(Id_Grupo)
 );
GO
 create table TAREA_ADJUNTO (
 --Id_TA int identity primary key not null,
 Id_Tarea_TA int,
 Url_Adjunto varchar(300) not null,
 foreign key (Id_Tarea_TA) references TAREA(Id_Tarea)
 );
GO

 create table PRUEBA (
  Id_Prueba int identity primary key not null,
  Tipo_Prueba char(1) not null check (Tipo_prueba in ('E', 'P')),
  --"E" Escrito, "P" Parcial
  Id_Materia_Prueba int not null,
  Id_Grupo_Prueba int not null,
  Id_Docente_Prueba int not null,
  Fecha_Prueba Date not null,
  Hora_Inicio_Prueba int not null,
  Hora_Fin_Prueba int not null,
  Temas_Prueba varchar(600) not null,
  foreign key (Id_Materia_Prueba) references MATERIA(Id_Materia),
  foreign key (Id_Grupo_Prueba) references GRUPO(Id_Grupo),
  foreign key (Id_Docente_Prueba) references DOCENTE(Id_Docente),
  foreign key (Hora_Inicio_Prueba) references HORA_TURNO(Id_HT),
  foreign key (Hora_Fin_Prueba) references HORA_TURNO(Id_HT)
  );
GO
  create table ENCUESTA (
  Id_Encuesta int identity primary key not null,
  Creador_Encuesta int not null,
  Fecha_Encuesta Datetime not null,
  Fecha_Fin_Encuesta Datetime not null,
  Titulo_Encuesta varchar(200) not null,
  Descripcion_Encuesta varchar(600) not null,
  foreign key (Creador_Encuesta) references USUARIO(Id_Usr)
  );
GO
  create table PREGUNTA (

  Id_Pregunta int identity primary key not null,
  Id_Encuesta_Pregunta int not null,
  Texto_Pregunta varchar(600) not null,
  foreign key (Id_Encuesta_Pregunta) references ENCUESTA(Id_Encuesta)
  );
GO
  create table OPCION_PREGUNTA (
  
  Id_OP int identity primary key not null,
  Id_Pregunta_OP int not null,
  Texto_OP varchar(600) not null,
  foreign key (Id_Pregunta_OP) references PREGUNTA(Id_Pregunta)
  );
GO
  
  
  
CREATE TABLE [dbo].[RESPUESTA_ENCUESTA] (
    [Id_Usuario_Encuestado]  INT NOT NULL,
    [Id_Pregunta_Respondida] INT NOT NULL,
    [Id_Respuesta]           INT NOT NULL,
    [Id_Respusta_Encuesta] INT NOT NULL, 
    FOREIGN KEY ([Id_Usuario_Encuestado]) REFERENCES [dbo].[USUARIO] ([Id_Usr]),
    FOREIGN KEY ([Id_Pregunta_Respondida]) REFERENCES [dbo].[PREGUNTA] ([Id_Pregunta]),
    FOREIGN KEY ([Id_Respuesta]) REFERENCES [dbo].[OPCION_PREGUNTA] ([Id_OP]), 
    CONSTRAINT [PK_RESPUESTA_ENCUESTA] PRIMARY KEY ([Id_Respusta_Encuesta])
);


GO

------------------------------------------------------------------------------------------------------
-------------------------------------    STORE PROCEDURES     ----------------------------------------
------------------------------------------------------------------------------------------------------

--Ingreso de un Usuario

create PROCEDURE dbo.PR_Ingresar_Usuario
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

CREATE PROCEDURE dbo.PR_Login
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
--Probamos el login
/*
DECLARE	@mensajeRespuesta varchar(250)

--Datos Correctos
EXEC	dbo.PR_Login
		@pCedula = '12345678',
		@pPassword ='1234',
		@mensajeRespuesta = @mensajeRespuesta OUTPUT

SELECT	@mensajeRespuesta as '@mensajeRespuesta'

--Cédula incorrecta
EXEC	dbo.PR_Login
		@pCedula = '92345678',
		@pPassword ='1234',
		@mensajeRespuesta = @mensajeRespuesta OUTPUT

SELECT	@mensajeRespuesta as '@mensajeRespuesta'

--Contraseña Incorrecta
EXEC	dbo.PR_Login
		@pCedula = '12345678',
		@pPassword ='x234',
		@mensajeRespuesta = @mensajeRespuesta OUTPUT

SELECT	@mensajeRespuesta as '@mensajeRespuesta'
*/

create PROCEDURE dbo.PR_Ingresar_Orientacion
    @Nombre_Orientacion varchar(30),
	@mensajeRespuesta varchar(10) output
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO dbo.[Orientacion] (Nombre_Orientacion)
        VALUES(@Nombre_Orientacion)

       SET @mensajeRespuesta='OK'
    END TRY
    BEGIN CATCH
        SET @mensajeRespuesta='ERROR' 
    END CATCH
END
go
create PROCEDURE dbo.PR_Ingresar_Grupo
    @Grado_Grupo int,
	@Orientacion_Grupo int,
	@Numero_Grupo int,
	@Turno_Grupo int,
	@Anio_Grupo int,
	@mensajeRespuesta varchar(10) output
AS
BEGIN
    SET NOCOUNT ON
    BEGIN TRY
        INSERT INTO dbo.[Grupo] (Grado_Grupo, Orientacion_Grupo, Numero_Grupo, Turno_Grupo, Anio_Grupo)
        VALUES(@Grado_Grupo, @Orientacion_Grupo, @Numero_Grupo, @Turno_Grupo, @Anio_Grupo)

       SET @mensajeRespuesta='OK'
    END TRY
    BEGIN CATCH
        SET @mensajeRespuesta='ERROR' 
    END CATCH
END
go

