using API.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public abstract class EventoGrupo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("EventoId")]
        public Event Evento { get; set; }
        public int EventoId { get; set; }

        public Grupo Grupo { get; set; }
        public int GrupoId { get; set; }

        public bool Notified { get; set; } = false;

    }
}
