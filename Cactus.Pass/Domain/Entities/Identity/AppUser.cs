using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<UserJwtToken> JwtTokens { get; set; }

        public virtual ICollection<Password> Passwords { get; set; }

        public virtual ICollection<Note> Messages { get; set; }
    }
}
