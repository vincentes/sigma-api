﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Alumno : SigmaUser
    {
        public virtual Grupo Group { get; set; }
    }
}
