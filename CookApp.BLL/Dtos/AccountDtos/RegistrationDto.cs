using CookApp.Entity;

namespace CookApp.BLL.Dtos.AccountDtos
{
    public class RegistrationDto
    {
        public int UserId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
