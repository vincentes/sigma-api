using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class SigmaUser
    {
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual IdentityUserExt User { get; set; }
        public virtual string UserId { get; set; }
        public virtual Docente UserData { get; set; }
        public bool IsUserType(Type type)
        {
            return type == typeof(UserData);
        }
    }
}
