﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class TareaGrupo : EventoGrupo
    {
        public Tarea Tarea {
            get
            {
                return (Tarea)Evento;
            }
        }
    }
}
