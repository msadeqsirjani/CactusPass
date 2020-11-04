using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
