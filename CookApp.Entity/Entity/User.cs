using CookApp.Entity;
using CookApp.Entity.Entity;

namespace CookApp.Entity.Entity
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public DateTime? LoginDate { get; set; }
        public bool IsAuthenticated { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
