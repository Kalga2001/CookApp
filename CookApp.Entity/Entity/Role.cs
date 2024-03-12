using CookApp.Entity;
using CookApp.Enums;

namespace CookApp.Models
{
    public class Role : BaseEntity
    {
        public RoleName RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
