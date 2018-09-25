using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class EscritoGrupo 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("EscritoId")]
        public Escrito Escrito { get; set; }
        public int EscritoId { get; set; }

        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }

        public bool Notified { get; set; } = false;
    }
}
