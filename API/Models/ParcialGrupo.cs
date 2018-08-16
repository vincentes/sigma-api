using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ParcialGrupo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("ParcialId")]
        public Parcial Parcial { get; set; }
        public int ParcialId { get; set; }

        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }
    }
}
