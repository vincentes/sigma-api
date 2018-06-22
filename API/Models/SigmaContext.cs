using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public partial class SigmaContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Materia> Materias { get; set; }
        public virtual DbSet<Grupo> Grupos { get; set; }
        public virtual DbSet<Orientacion> Orientaciones { get; set; }
        public virtual DbSet<Docente> Docentes { get; set; }
        public virtual DbSet<Alumno> Alumnos { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<GrupoDocente> GrupoDocente { get; set; }

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

            builder.Entity<Grupo>()
                .HasOne(d => d.Orientacion)
                .WithMany(p => p.Grupos)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<Orientacion>()
                .HasMany(d => d.Grupos)
                .WithOne(p => p.Orientacion)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<GrupoDocente>()
                .HasOne(d => d.Grupo)
                .WithMany(p => p.GrupoDocentes)
                .HasForeignKey(d => d.GrupoId);

            builder.Entity<GrupoDocente>()
                .HasOne(d => d.Docente)
                .WithMany(p => p.GrupoDocentes)
                .HasForeignKey(d => d.DocenteId);

            builder.Entity<Orientacion>()
                .HasMany(d => d.Materias)
                .WithOne(p => p.Orientacion)
                .HasForeignKey(d => d.OrientacionId);

            builder.Entity<Docente>()
                .HasOne(d => d.Materia)
                .WithMany(p => p.Docentes)
                .HasForeignKey(d => d.MateriaId);
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
