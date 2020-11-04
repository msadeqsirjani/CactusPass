using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Password : BaseEntity, IPasswordHashProperty
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string HashedPassword { get; set; }
        public string UsedIn { get; set; }
        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
