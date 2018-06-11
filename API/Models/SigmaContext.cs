using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class SigmaContext : IdentityDbContext
    {
        public SigmaContext(DbContextOptions options)
            : base(options)
        {

        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Adscripto> Adscripto { get; set; }
        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<Beacon> Beacon { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<Encuesta> Encuesta { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<Hora> Hora { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<HoraTurno> HoraTurno { get; set; }
        public virtual DbSet<Materia> Materia { get; set; }
        public virtual DbSet<Nodo> Nodo { get; set; }
        public virtual DbSet<Notificacion> Notificacion { get; set; }
        public virtual DbSet<NotificacionDestinatario> NotificacionDestinatario { get; set; }
        public virtual DbSet<OpcionPregunta> OpcionPregunta { get; set; }
        public virtual DbSet<Orientacion> Orientacion { get; set; }
        public virtual DbSet<Ponderacion> Ponderacion { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Prueba> Prueba { get; set; }
        public virtual DbSet<RespuestaEncuesta> RespuestaEncuesta { get; set; }
        public virtual DbSet<Tarea> Tarea { get; set; }
        public virtual DbSet<TareaGrupo> TareaGrupo { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        // Unable to generate entity type for table 'dbo.TAREA_ADJUNTO'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=vincentes-pc\\vincentex;Database=Sigma;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador);

                entity.ToTable("ADMINISTRADOR");

                entity.Property(e => e.IdAdministrador).HasColumnName("Id_Administrador");

                entity.Property(e => e.IdUsrAdmin).HasColumnName("Id_Usr_Admin");

                entity.HasOne(d => d.IdUsrAdminNavigation)
                    .WithMany(p => p.Administrador)
                    .HasForeignKey(d => d.IdUsrAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADMINISTR__Id_Us__2F10007B");
            });

            modelBuilder.Entity<Adscripto>(entity =>
            {
                entity.HasKey(e => e.IdAdscripto);

                entity.ToTable("ADSCRIPTO");

                entity.Property(e => e.IdAdscripto).HasColumnName("Id_Adscripto");

                entity.Property(e => e.IdUsrAdscripto).HasColumnName("Id_Usr_Adscripto");

                entity.Property(e => e.TurnoAdscripto).HasColumnName("Turno_Adscripto");

                entity.HasOne(d => d.IdUsrAdscriptoNavigation)
                    .WithMany(p => p.Adscripto)
                    .HasForeignKey(d => d.IdUsrAdscripto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADSCRIPTO__Id_Us__2B3F6F97");

                entity.HasOne(d => d.TurnoAdscriptoNavigation)
                    .WithMany(p => p.Adscripto)
                    .HasForeignKey(d => d.TurnoAdscripto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADSCRIPTO__Turno__2C3393D0");
            });

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasKey(e => e.IdAlumno);

                entity.ToTable("ALUMNO");

                entity.Property(e => e.IdAlumno).HasColumnName("Id_Alumno");

                entity.Property(e => e.GrupoUsr).HasColumnName("Grupo_Usr");

                entity.Property(e => e.IdUsrAlumno).HasColumnName("Id_Usr_Alumno");

                entity.HasOne(d => d.GrupoUsrNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.GrupoUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ALUMNO__Grupo_Us__24927208");

                entity.HasOne(d => d.IdUsrAlumnoNavigation)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.IdUsrAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ALUMNO__Id_Usr_A__239E4DCF");
            });

            modelBuilder.Entity<Beacon>(entity =>
            {
                entity.HasKey(e => new { e.IdBeacon, e.IdNodoBeacon });

                entity.ToTable("BEACON");

                entity.Property(e => e.IdBeacon)
                    .HasColumnName("Id_Beacon")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.IdNodoBeacon).HasColumnName("Id_Nodo_Beacon");

                entity.Property(e => e.StatusNodo)
                    .IsRequired()
                    .HasColumnName("Status_Nodo")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNodoBeaconNavigation)
                    .WithMany(p => p.Beacon)
                    .HasForeignKey(d => d.IdNodoBeacon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BEACON__Id_Nodo___35BCFE0A");
            });

            modelBuilder.Entity<Docente>(entity =>
            {
                entity.HasKey(e => e.IdDocente);

                entity.ToTable("DOCENTE");

                entity.Property(e => e.IdDocente).HasColumnName("Id_Docente");

                entity.Property(e => e.IdUsrDocente).HasColumnName("Id_Usr_Docente");

                entity.Property(e => e.MateriaDocente).HasColumnName("Materia_Docente");

                entity.HasOne(d => d.IdUsrDocenteNavigation)
                    .WithMany(p => p.Docente)
                    .HasForeignKey(d => d.IdUsrDocente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DOCENTE__Id_Usr___276EDEB3");

                entity.HasOne(d => d.MateriaDocenteNavigation)
                    .WithMany(p => p.Docente)
                    .HasForeignKey(d => d.MateriaDocente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DOCENTE__Materia__286302EC");
            });

            modelBuilder.Entity<Encuesta>(entity =>
            {
                entity.HasKey(e => e.IdEncuesta);

                entity.ToTable("ENCUESTA");

                entity.Property(e => e.IdEncuesta).HasColumnName("Id_Encuesta");

                entity.Property(e => e.CreadorEncuesta).HasColumnName("Creador_Encuesta");

                entity.Property(e => e.DescripcionEncuesta)
                    .IsRequired()
                    .HasColumnName("Descripcion_Encuesta")
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEncuesta)
                    .HasColumnName("Fecha_Encuesta")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaFinEncuesta)
                    .HasColumnName("Fecha_Fin_Encuesta")
                    .HasColumnType("datetime");

                entity.Property(e => e.TituloEncuesta)
                    .IsRequired()
                    .HasColumnName("Titulo_Encuesta")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreadorEncuestaNavigation)
                    .WithMany(p => p.Encuesta)
                    .HasForeignKey(d => d.CreadorEncuesta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENCUESTA__Creado__5FB337D6");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo);

                entity.ToTable("GRUPO");

                entity.Property(e => e.IdGrupo).HasColumnName("Id_Grupo");

                entity.Property(e => e.AnioGrupo).HasColumnName("Anio_grupo");

                entity.Property(e => e.GradoGrupo).HasColumnName("Grado_Grupo");

                entity.Property(e => e.NumeroGrupo).HasColumnName("Numero_Grupo");

                entity.Property(e => e.OrientacionGrupo).HasColumnName("Orientacion_Grupo");

                entity.Property(e => e.TurnoGrupo).HasColumnName("Turno_Grupo");

                entity.HasOne(d => d.OrientacionGrupoNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.OrientacionGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GRUPO__Orientaci__1CF15040");

                entity.HasOne(d => d.TurnoGrupoNavigation)
                    .WithMany(p => p.Grupo)
                    .HasForeignKey(d => d.TurnoGrupo)
                    .HasConstraintName("FK__GRUPO__Turno_Gru__1DE57479");
            });

            modelBuilder.Entity<Hora>(entity =>
            {
                entity.HasKey(e => e.IdHora);

                entity.ToTable("HORA");

                entity.Property(e => e.IdHora).HasColumnName("Id_Hora");

                entity.Property(e => e.FinHora).HasColumnName("Fin_Hora");

                entity.Property(e => e.InicioHora).HasColumnName("Inicio_Hora");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => new { e.IdGrupoHorario, e.DiaHorario, e.HoraHorario });

                entity.ToTable("HORARIO");

                entity.Property(e => e.IdGrupoHorario).HasColumnName("Id_Grupo_Horario");

                entity.Property(e => e.DiaHorario)
                    .HasColumnName("Dia_Horario")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.HoraHorario).HasColumnName("Hora_Horario");

                entity.Property(e => e.IdDocenteHorario).HasColumnName("Id_Docente_Horario");

                entity.Property(e => e.IdMateriaHorario).HasColumnName("Id_Materia_Horario");

                entity.Property(e => e.IdSalon).HasColumnName("Id_Salon");

                entity.HasOne(d => d.HoraHorarioNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.HoraHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORARIO__Hora_Ho__3E52440B");

                entity.HasOne(d => d.IdDocenteHorarioNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.IdDocenteHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORARIO__Id_Doce__403A8C7D");

                entity.HasOne(d => d.IdGrupoHorarioNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.IdGrupoHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORARIO__Id_Grup__3D5E1FD2");

                entity.HasOne(d => d.IdMateriaHorarioNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.IdMateriaHorario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORARIO__Id_Mate__3F466844");

                entity.HasOne(d => d.IdSalonNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.IdSalon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORARIO__Id_Salo__412EB0B6");
            });

            modelBuilder.Entity<HoraTurno>(entity =>
            {
                entity.HasKey(e => e.IdHt);

                entity.ToTable("HORA_TURNO");

                entity.Property(e => e.IdHt).HasColumnName("Id_HT");

                entity.Property(e => e.AliasHt)
                    .IsRequired()
                    .HasColumnName("Alias_HT")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HoraHt).HasColumnName("Hora_HT");

                entity.Property(e => e.TurnoHt).HasColumnName("Turno_HT");

                entity.HasOne(d => d.HoraHtNavigation)
                    .WithMany(p => p.HoraTurno)
                    .HasForeignKey(d => d.HoraHt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORA_TURN__Hora___164452B1");

                entity.HasOne(d => d.TurnoHtNavigation)
                    .WithMany(p => p.HoraTurno)
                    .HasForeignKey(d => d.TurnoHt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HORA_TURN__Turno__173876EA");
            });

            modelBuilder.Entity<Materia>(entity =>
            {
                entity.HasKey(e => e.IdMateria);

                entity.ToTable("MATERIA");

                entity.Property(e => e.IdMateria).HasColumnName("Id_Materia");

                entity.Property(e => e.NombreMateria)
                    .IsRequired()
                    .HasColumnName("Nombre_Materia")
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.OrientacionMateria).HasColumnName("Orientacion_Materia");

                entity.HasOne(d => d.OrientacionMateriaNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.OrientacionMateria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MATERIA__Orienta__1A14E395");
            });

            modelBuilder.Entity<Nodo>(entity =>
            {
                entity.HasKey(e => e.IdNodo);

                entity.ToTable("NODO");

                entity.Property(e => e.IdNodo).HasColumnName("Id_Nodo");

                entity.Property(e => e.PisoNodo)
                    .IsRequired()
                    .HasColumnName("Piso_Nodo")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.PosXNodo).HasColumnName("PosX_Nodo");

                entity.Property(e => e.PosYNodo).HasColumnName("PosY_Nodo");

                entity.Property(e => e.TipoNodo)
                    .IsRequired()
                    .HasColumnName("Tipo_Nodo")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion);

                entity.ToTable("NOTIFICACION");

                entity.Property(e => e.IdNotificacion).HasColumnName("Id_Notificacion");

                entity.Property(e => e.DestinoNotificacion)
                    .IsRequired()
                    .HasColumnName("Destino_Notificacion")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNotificacion)
                    .HasColumnName("Fecha_Notificacion")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MensajeNotificacion)
                    .IsRequired()
                    .HasColumnName("Mensaje_Notificacion")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TituloNotificacion)
                    .IsRequired()
                    .HasColumnName("Titulo_Notificacion")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NotificacionDestinatario>(entity =>
            {
                entity.HasKey(e => new { e.IdNd, e.IdDestinatario });

                entity.ToTable("NOTIFICACION_DESTINATARIO");

                entity.Property(e => e.IdNd).HasColumnName("Id_ND");

                entity.Property(e => e.IdDestinatario).HasColumnName("Id_Destinatario");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("Fecha_Entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaEnvio)
                    .HasColumnName("Fecha_Envio")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GrupoActualUsr).HasColumnName("Grupo_Actual_Usr");

                entity.HasOne(d => d.GrupoActualUsrNavigation)
                    .WithMany(p => p.NotificacionDestinatario)
                    .HasForeignKey(d => d.GrupoActualUsr)
                    .HasConstraintName("FK__NOTIFICAC__Grupo__4AB81AF0");

                entity.HasOne(d => d.IdDestinatarioNavigation)
                    .WithMany(p => p.NotificacionDestinatario)
                    .HasForeignKey(d => d.IdDestinatario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NOTIFICAC__Id_De__49C3F6B7");

                entity.HasOne(d => d.IdNdNavigation)
                    .WithMany(p => p.NotificacionDestinatario)
                    .HasForeignKey(d => d.IdNd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__NOTIFICAC__Id_ND__48CFD27E");
            });

            modelBuilder.Entity<OpcionPregunta>(entity =>
            {
                entity.HasKey(e => e.IdOp);

                entity.ToTable("OPCION_PREGUNTA");

                entity.Property(e => e.IdOp).HasColumnName("Id_OP");

                entity.Property(e => e.IdPreguntaOp).HasColumnName("Id_Pregunta_OP");

                entity.Property(e => e.TextoOp)
                    .IsRequired()
                    .HasColumnName("Texto_OP")
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPreguntaOpNavigation)
                    .WithMany(p => p.OpcionPregunta)
                    .HasForeignKey(d => d.IdPreguntaOp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OPCION_PR__Id_Pr__656C112C");
            });

            modelBuilder.Entity<Orientacion>(entity =>
            {
                entity.HasKey(e => e.IdOrientacion);

                entity.ToTable("ORIENTACION");

                entity.Property(e => e.IdOrientacion).HasColumnName("Id_Orientacion");

                entity.Property(e => e.NombreOrientacion)
                    .IsRequired()
                    .HasColumnName("Nombre_Orientacion")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ponderacion>(entity =>
            {
                entity.HasKey(e => new { e.IdNodoActual, e.IdNodoAdyacente });

                entity.ToTable("PONDERACION");

                entity.Property(e => e.IdNodoActual).HasColumnName("Id_Nodo_Actual");

                entity.Property(e => e.IdNodoAdyacente).HasColumnName("Id_Nodo_Adyacente");

                entity.Property(e => e.Ponderacion1).HasColumnName("Ponderacion");

                entity.HasOne(d => d.IdNodoActualNavigation)
                    .WithMany(p => p.PonderacionIdNodoActualNavigation)
                    .HasForeignKey(d => d.IdNodoActual)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PONDERACI__Id_No__38996AB5");

                entity.HasOne(d => d.IdNodoAdyacenteNavigation)
                    .WithMany(p => p.PonderacionIdNodoAdyacenteNavigation)
                    .HasForeignKey(d => d.IdNodoAdyacente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PONDERACI__Id_No__398D8EEE");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.HasKey(e => e.IdPregunta);

                entity.ToTable("PREGUNTA");

                entity.Property(e => e.IdPregunta).HasColumnName("Id_Pregunta");

                entity.Property(e => e.IdEncuestaPregunta).HasColumnName("Id_Encuesta_Pregunta");

                entity.Property(e => e.TextoPregunta)
                    .IsRequired()
                    .HasColumnName("Texto_Pregunta")
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEncuestaPreguntaNavigation)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.IdEncuestaPregunta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREGUNTA__Id_Enc__628FA481");
            });

            modelBuilder.Entity<Prueba>(entity =>
            {
                entity.HasKey(e => e.IdPrueba);

                entity.ToTable("PRUEBA");

                entity.Property(e => e.IdPrueba).HasColumnName("Id_Prueba");

                entity.Property(e => e.FechaPrueba)
                    .HasColumnName("Fecha_Prueba")
                    .HasColumnType("date");

                entity.Property(e => e.HoraFinPrueba).HasColumnName("Hora_Fin_Prueba");

                entity.Property(e => e.HoraInicioPrueba).HasColumnName("Hora_Inicio_Prueba");

                entity.Property(e => e.IdDocentePrueba).HasColumnName("Id_Docente_Prueba");

                entity.Property(e => e.IdGrupoPrueba).HasColumnName("Id_Grupo_Prueba");

                entity.Property(e => e.IdMateriaPrueba).HasColumnName("Id_Materia_Prueba");

                entity.Property(e => e.TemasPrueba)
                    .IsRequired()
                    .HasColumnName("Temas_Prueba")
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPrueba)
                    .IsRequired()
                    .HasColumnName("Tipo_Prueba")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.HoraFinPruebaNavigation)
                    .WithMany(p => p.PruebaHoraFinPruebaNavigation)
                    .HasForeignKey(d => d.HoraFinPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRUEBA__Hora_Fin__5CD6CB2B");

                entity.HasOne(d => d.HoraInicioPruebaNavigation)
                    .WithMany(p => p.PruebaHoraInicioPruebaNavigation)
                    .HasForeignKey(d => d.HoraInicioPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRUEBA__Hora_Ini__5BE2A6F2");

                entity.HasOne(d => d.IdDocentePruebaNavigation)
                    .WithMany(p => p.Prueba)
                    .HasForeignKey(d => d.IdDocentePrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRUEBA__Id_Docen__5AEE82B9");

                entity.HasOne(d => d.IdGrupoPruebaNavigation)
                    .WithMany(p => p.Prueba)
                    .HasForeignKey(d => d.IdGrupoPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRUEBA__Id_Grupo__59FA5E80");

                entity.HasOne(d => d.IdMateriaPruebaNavigation)
                    .WithMany(p => p.Prueba)
                    .HasForeignKey(d => d.IdMateriaPrueba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PRUEBA__Id_Mater__59063A47");
            });

            modelBuilder.Entity<RespuestaEncuesta>(entity =>
            {
                entity.HasKey(e => e.IdRespustaEncuesta);

                entity.ToTable("RESPUESTA_ENCUESTA");

                entity.Property(e => e.IdRespustaEncuesta).HasColumnName("Id_Respusta_Encuesta");

                entity.Property(e => e.IdPreguntaRespondida).HasColumnName("Id_Pregunta_Respondida");

                entity.Property(e => e.IdRespuesta).HasColumnName("Id_Respuesta");

                entity.Property(e => e.IdUsuarioEncuestado).HasColumnName("Id_Usuario_Encuestado");

                entity.HasOne(d => d.IdPreguntaRespondidaNavigation)
                    .WithMany(p => p.RespuestaEncuesta)
                    .HasForeignKey(d => d.IdPreguntaRespondida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESPUESTA__Id_Pr__75A278F5");

                entity.HasOne(d => d.IdRespuestaNavigation)
                    .WithMany(p => p.RespuestaEncuesta)
                    .HasForeignKey(d => d.IdRespuesta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESPUESTA__Id_Re__76969D2E");

                entity.HasOne(d => d.IdUsuarioEncuestadoNavigation)
                    .WithMany(p => p.RespuestaEncuesta)
                    .HasForeignKey(d => d.IdUsuarioEncuestado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RESPUESTA__Id_Us__74AE54BC");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTarea);

                entity.ToTable("TAREA");

                entity.Property(e => e.IdTarea).HasColumnName("Id_Tarea");

                entity.Property(e => e.ContenidoTarea)
                    .IsRequired()
                    .HasColumnName("Contenido_Tarea")
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.IdDocente).HasColumnName("Id_Docente");

                entity.Property(e => e.IdMateriaTarea).HasColumnName("Id_Materia_Tarea");

                entity.HasOne(d => d.IdDocenteNavigation)
                    .WithMany(p => p.Tarea)
                    .HasForeignKey(d => d.IdDocente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TAREA__Id_Docent__4D94879B");

                entity.HasOne(d => d.IdMateriaTareaNavigation)
                    .WithMany(p => p.Tarea)
                    .HasForeignKey(d => d.IdMateriaTarea)
                    .HasConstraintName("FK__TAREA__Id_Materi__4E88ABD4");
            });

            modelBuilder.Entity<TareaGrupo>(entity =>
            {
                entity.HasKey(e => new { e.IdTg, e.IdGrupoTg });

                entity.ToTable("TAREA_GRUPO");

                entity.Property(e => e.IdTg).HasColumnName("Id_TG");

                entity.Property(e => e.IdGrupoTg).HasColumnName("Id_Grupo_TG");

                entity.Property(e => e.FechaEntregaTg)
                    .HasColumnName("Fecha_Entrega_TG")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaTg)
                    .HasColumnName("Fecha_TG")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdGrupoTgNavigation)
                    .WithMany(p => p.TareaGrupo)
                    .HasForeignKey(d => d.IdGrupoTg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TAREA_GRU__Id_Gr__534D60F1");

                entity.HasOne(d => d.IdTgNavigation)
                    .WithMany(p => p.TareaGrupo)
                    .HasForeignKey(d => d.IdTg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TAREA_GRU__Id_TG__52593CB8");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno);

                entity.ToTable("TURNO");

                entity.Property(e => e.IdTurno).HasColumnName("Id_Turno");

                entity.Property(e => e.NombreTurno)
                    .IsRequired()
                    .HasColumnName("Nombre_Turno")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsr);

                entity.ToTable("USUARIO");

                entity.HasIndex(e => e.CedulaUsr)
                    .HasName("UQ__USUARIO__8F289EF0749E37D8")
                    .IsUnique();

                entity.Property(e => e.IdUsr).HasColumnName("Id_Usr");

                entity.Property(e => e.ApellidoUsr)
                    .HasColumnName("Apellido_Usr")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CedulaUsr)
                    .IsRequired()
                    .HasColumnName("Cedula_Usr")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CelularUsr)
                    .HasColumnName("Celular_Usr")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsr)
                    .HasColumnName("Nombre_Usr")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PassHashUsr)
                    .IsRequired()
                    .HasColumnName("PassHash_Usr")
                    .HasMaxLength(64);

                entity.Property(e => e.TokenUsr)
                    .HasColumnName("Token_Usr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
