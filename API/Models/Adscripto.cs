using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Adscripto : AppUser
    {
        public List<EncuestaGlobal> Encuestas { get; set; }
    }
}
