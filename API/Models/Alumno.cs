﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Alumno : IdentityUser
    {
        public virtual Grupo Group { get; set; }
    }
}
