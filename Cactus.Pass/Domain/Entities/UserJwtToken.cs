using Domain.Common;
using Domain.Entities.Enums;
using Domain.Entities.Identity;
using System;

namespace Domain.Entities
{
    public class UserJwtToken : BaseEntity
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpireDateTime { get; set; }
        public Platform Platform { get; set; }
        public string UserId { get; set; }

        public AppUser User { get; set; }
    }
}
