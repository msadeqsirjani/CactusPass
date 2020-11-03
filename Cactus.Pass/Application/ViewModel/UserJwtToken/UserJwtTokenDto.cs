using Domain.Entities.Enums;
using System;

namespace Application.ViewModel.UserJwtToken
{
    public class UserJwtTokenDto
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpireDateTime { get; set; }
        public Platform TokenPlatform { get; set; }
    }
}
