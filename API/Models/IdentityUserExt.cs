using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class IdentityUserExt : IdentityUser
    {
        public virtual SigmaUser SigmaUser { get; set; }
        public virtual int SigmaUserId { get; set; }
    }
}
