using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class SigmaContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Alerta> Alertas { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<Orientacion> Orientaciones { get; set; }
        public virtual DbSet<Docente> Docentes { get; set; }
        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<API.Models.GrupoDocente> GrupoDocente { get; set; }
        public virtual DbSet<API.Models.MateriaOrientacion> MateriaOrientacion { get; set; }
        public virtual DbSet<API.Models.Imagen> Imagen { get; set; }
        public virtual DbSet<API.Models.TareaImagen> TareaImagen { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<TareaGrupo> TareaGrupo { get; set; }
        public virtual DbSet<ParcialGrupo> ParcialGrupo { get; set; }
        public virtual DbSet<EscritoGrupo> EscritoGrupo { get; set; }
        public virtual DbSet<Alumno> Students { get; set; }
        public virtual DbSet<Parcial> Parciales { get; set; }
        public virtual DbSet<Escrito> Escritos { get; set; }
        public virtual DbSet<EncuestaGlobal> EncuestasGlobales { get; set; }
        public virtual DbSet<Adscripto> Adscriptos { get; set; }
        public virtual DbSet<Pregunta> Preguntas { get; set; }
        public virtual DbSet<PreguntaVariada> PreguntasVariadas { get; set; }
        public virtual DbSet<PreguntaLibre> PreguntasLibres { get; set; }
        public virtual DbSet<PreguntaMO> PreguntasMO { get; set; }
        public virtual DbSet<PreguntaMO> PreguntasEL { get; set; }
        public virtual DbSet<PreguntaMO> PreguntasUO { get; set; }
        public virtual DbSet<Respuesta> Respuestas { get; set; }
        public virtual DbSet<RespuestaLibre> RespuestasLibres { get; set; }
        public virtual DbSet<RespuestaLimitada> RespuestasLimitadas { get; set; }
        public virtual DbSet<PreguntaOpcion> PreguntaOpciones { get; set; }
        public virtual DbSet<CambioDeSalon> CambioDeSalones { get; set; }
        public virtual DbSet<Aviso> Avisos { get; set; }
        public virtual DbSet<HoraMateria> HoraMaterias { get; set; }
        public virtual DbSet<Salon> Salones { get; set; }
        public SigmaContext(DbContextOptions options) : base(options)
        {

        }

        public SigmaContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Grupo>().HasKey(e => e.Id);
            builder.Entity<Orientacion>().HasKey(e => e.Id);
            builder.Entity<Turno>().HasKey(e => e.Id);
            builder.Entity<Materia>().HasKey(e => e.Id);
            builder.Entity<Turno>().HasKey(e => e.Id);
            builder.Entity<MateriaOrientacion>().HasKey(e => e.Id);
            builder.Entity<Materia>().HasKey(e => e.Id);
            builder.Entity<Imagen>().HasKey(e => e.Id);
            builder.Entity<GrupoDocente>().HasKey(e => e.Id);
            builder.Entity<Token>().HasKey(e => e.Id);

            builder.Entity<Grupo>()
                .HasOne(d => d.Orientacion)
                .WithMany(p => p.Grupos)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<Grupo>()
                .HasOne(d => d.Turno)
                .WithMany(p => p.Grupos)
                .HasForeignKey(d => d.TurnoId);

            builder.Entity<Grupo>()
                .HasOne(d => d.Turno)
                .WithMany(p => p.Grupos)
                .HasForeignKey(d => d.TurnoId);

            builder.Entity<Grupo>()
                .HasMany(d => d.Alumnos)
                .WithOne(p => p.Grupo)
                .HasForeignKey(d => d.GrupoId);


            builder.Entity<Orientacion>()
                .HasMany(d => d.Grupos)
                .WithOne(p => p.Orientacion)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<GrupoDocente>()
                .HasOne(d => d.Docente)
                .WithMany(p => p.GrupoDocentes)
                .HasForeignKey(d => d.DocenteId);

            builder.Entity<Docente>()
                .HasOne(d => d.Materia)
                .WithMany(p => p.Docentes)
                .HasForeignKey(d => d.MateriaId);

            builder.Entity<GrupoDocente>()
                .HasOne(d => d.Grupo)
                .WithMany(p => p.GrupoDocentes)
                .HasForeignKey(d => d.GrupoId);

            builder.Entity<GrupoDocente>()
                .HasOne(d => d.Docente)
                .WithMany(p => p.GrupoDocentes)
                .HasForeignKey(d => d.DocenteId);

            builder.Entity<MateriaOrientacion>()
                .HasOne(d => d.Materia)
                .WithMany(p => p.MateriaOrientacion)
                .HasForeignKey(d => d.MateriaId);

            builder.Entity<MateriaOrientacion>()
                .HasOne(d => d.Orientacion)
                .WithMany(p => p.MateriaOrientacion)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<TareaImagen>()
                .HasOne(d => d.Imagen)
                .WithMany(p => p.TareaImagen)
                .HasForeignKey(d => d.ImagenId);

            builder.Entity<TareaImagen>()
                .HasOne(d => d.Tarea)
                .WithMany(p => p.TareaImagen)
                .HasForeignKey(d => d.TareaId);

            builder.Entity<Tarea>()
                .HasOne(d => d.Materia)
                .WithMany(p => p.Tareas)
                .HasForeignKey(d => d.MateriaId);

            builder.Entity<Tarea>()
                .HasOne(d => d.Docente)
                .WithMany(p => p.Tareas)
                .HasForeignKey(d => d.DocenteId);

            builder.Entity<AppUser>()
                .HasMany(d => d.Token)
                .WithOne(p => p.User);

            builder.Entity<ParcialGrupo>()
                .HasOne(d => d.Grupo)
                .WithMany(p => p.ParcialGrupo)
                .HasForeignKey(d => d.GrupoId);

            builder.Entity<EscritoGrupo>()
                .HasOne(d => d.Grupo)
                .WithMany(p => p.EscritoGrupo)
                .HasForeignKey(d => d.GrupoId);

            builder.Entity<TareaGrupo>()
                .HasOne(d => d.Grupo)
                .WithMany(p => p.TareaGrupo)
                .HasForeignKey(d => d.GrupoId);


            builder.Entity<EncuestaGlobal>()
                .HasOne(d => d.Adscripto)
                .WithMany(p => p.Encuestas)
                .HasForeignKey(d => d.AdscriptoId);

            builder.Entity<PreguntaOpcion>()
                .HasOne(e => e.Pregunta)
                .WithMany(p => p.Opciones)
                .HasForeignKey(e => e.PreguntaId);

            builder.Entity<Respuesta>()
                .HasOne(e => e.Pregunta);

            builder.Entity<OpcionRespuesta>()
                .HasOne(e => e.Opcion)
                .WithMany(p => p.OpcionRespuestas)
                .HasForeignKey(e => e.Id);

            builder.Entity<OpcionRespuesta>()
                .HasOne(e => e.Respuesta)
                .WithMany(p => p.Respuestas)
                .HasForeignKey(e => e.Id);

            builder.Entity<Respuesta>()
                .HasOne(e => e.Alumno)
                .WithMany(p => p.Respuestas)
                .HasForeignKey(e => e.AlumnoId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=vincentes-pc\\vincentex;Database=Sigma;Trusted_Connection=True;");
            }
        }
    }
}
