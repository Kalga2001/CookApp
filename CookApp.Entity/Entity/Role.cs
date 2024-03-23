using CookApp.Entity;
using CookApp.Entity.Enums;

namespace CookApp.Entity.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
