using CookApp.Entity;

namespace CookApp.Entity.Entity
{
    public class UserRole : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
