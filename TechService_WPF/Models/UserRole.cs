using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserServices = new HashSet<UserService>();
        }

        public int IdUserRole { get; set; }
        public string NameUserRole { get; set; } = null!;

        public virtual ICollection<UserService> UserServices { get; set; }
    }
}
