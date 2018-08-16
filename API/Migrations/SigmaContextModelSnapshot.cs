﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(SigmaContext))]
    partial class SigmaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Anio");

                    b.Property<string>("DocenteId");

                    b.Property<int>("Grado");

                    b.Property<int>("Numero");

                    b.Property<int>("OrientacionId");

                    b.Property<int>("TurnoId");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("OrientacionId");

                    b.HasIndex("TurnoId");

                    b.ToTable("Grupos");
                });

            modelBuilder.Entity("API.Models.GrupoDocente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DocenteId");

                    b.Property<int>("GrupoId");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("GrupoId");

                    b.ToTable("GrupoDocente");
                });

            modelBuilder.Entity("API.Models.Imagen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Imagen");
                });

            modelBuilder.Entity("API.Models.Materia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("API.Models.MateriaOrientacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MateriaId");

                    b.Property<int>("OrientacionId");

                    b.HasKey("Id");

                    b.HasIndex("MateriaId");

                    b.HasIndex("OrientacionId");

                    b.ToTable("MateriaOrientacion");
                });

            modelBuilder.Entity("API.Models.Orientacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Orientaciones");
                });

            modelBuilder.Entity("API.Models.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contenido");

                    b.Property<string>("DocenteId");

                    b.Property<int>("MateriaId");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("API.Models.TareaGrupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Deadline");

                    b.Property<int>("GrupoId");

                    b.Property<int>("TareaId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("TareaId");

                    b.ToTable("TareaGrupo");
                });

            modelBuilder.Entity("API.Models.TareaImagen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ImagenId");

                    b.Property<int>("TareaId");

                    b.HasKey("Id");

                    b.HasIndex("ImagenId");

                    b.HasIndex("TareaId");

                    b.ToTable("TareaImagen");
                });

            modelBuilder.Entity("API.Models.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("API.Models.Turno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("API.Models.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");


                    b.ToTable("AppUser");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("API.Models.Admin", b =>
                {
                    b.HasBaseType("API.Models.AppUser");


                    b.ToTable("Admin");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("API.Models.Alumno", b =>
                {
                    b.HasBaseType("API.Models.AppUser");

                    b.Property<int>("GrupoId");

                    b.HasIndex("GrupoId");

                    b.ToTable("Alumno");

                    b.HasDiscriminator().HasValue("Alumno");
                });

            modelBuilder.Entity("API.Models.Docente", b =>
                {
                    b.HasBaseType("API.Models.AppUser");

                    b.Property<int>("MateriaId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Docente");

                    b.HasDiscriminator().HasValue("Docente");
                });

            modelBuilder.Entity("API.Models.Grupo", b =>
                {
                    b.HasOne("API.Models.Docente")
                        .WithMany("Grupos")
                        .HasForeignKey("DocenteId");

                    b.HasOne("API.Models.Orientacion", "Orientacion")
                        .WithMany("Grupos")
                        .HasForeignKey("OrientacionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Turno", "Turno")
                        .WithMany("Grupos")
                        .HasForeignKey("TurnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.GrupoDocente", b =>
                {
                    b.HasOne("API.Models.Docente", "Docente")
                        .WithMany("GrupoDocentes")
                        .HasForeignKey("DocenteId");

                    b.HasOne("API.Models.Grupo", "Grupo")
                        .WithMany("GrupoDocentes")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.MateriaOrientacion", b =>
                {
                    b.HasOne("API.Models.Materia", "Materia")
                        .WithMany("MateriaOrientacion")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Orientacion", "Orientacion")
                        .WithMany("MateriaOrientacion")
                        .HasForeignKey("OrientacionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Tarea", b =>
                {
                    b.HasOne("API.Models.Docente", "Docente")
                        .WithMany("Tareas")
                        .HasForeignKey("DocenteId");

                    b.HasOne("API.Models.Materia", "Materia")
                        .WithMany("Tareas")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.TareaGrupo", b =>
                {
                    b.HasOne("API.Models.Grupo", "Grupo")
                        .WithMany("TareaGrupo")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Tarea", "Tarea")
                        .WithMany("TareaGrupos")
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.TareaImagen", b =>
                {
                    b.HasOne("API.Models.Imagen", "Imagen")
                        .WithMany("TareaImagen")
                        .HasForeignKey("ImagenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.Tarea", "Tarea")
                        .WithMany("TareaImagen")
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Token", b =>
                {
                    b.HasOne("API.Models.AppUser", "User")
                        .WithMany("Token")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Alumno", b =>
                {
                    b.HasOne("API.Models.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Docente", b =>
                {
                    b.HasOne("API.Models.Materia", "Materia")
                        .WithMany("Docentes")
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
