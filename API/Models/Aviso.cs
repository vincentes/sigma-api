using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Aviso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }
    }   

    public class CambioDeSalon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("CreatorId")]
        public AppUser Creator { get; set; }
        public string CreatorId { get; set; }
        public int SalonInicialId { get; set; }
        [ForeignKey("SalonInicialId")]
        public Salon SalonInicial { get; set; }
        public int SalonCambioId { get; set; }
        [ForeignKey("SalonCambioId")]
        public Salon SalonCambio { get; set; }
        public int SalonFinalId { get; set; }
        [ForeignKey("SalonFinalId")]
        public Salon SalonFinal { get; set; }
        public int HoraMateriaInicialId { get; set; }
        [ForeignKey("HoraMateriaInicialId")]
        public HoraMateria HoraMateriaInicial { get; set; }
        public int HoraMateriaFinalId { get; set; }
        [ForeignKey("HoraMateriaFinalId")]
        public HoraMateria HoraMateriaFinal { get; set; }
    }

    public class HoraMateria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Hora { get; set; }
        public int SalonId { get; set; }
        public string Dia { get; set; }
        [ForeignKey("SalonId")]
        public Salon Salon { get; set; }
        public int MateriaId { get; set; }
        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }
        public int GrupoId { get; set; }
        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }
    }

    public class Salon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
