using Domain.Entities.Enums;

namespace Application.ViewModel.Account
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Platform Platform { get; set; }
    }
}
