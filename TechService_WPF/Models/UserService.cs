using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class UserService
    {
        public UserService()
        {
            Executors = new HashSet<Executor>();
        }

        public int IdUser { get; set; }
        public string SurnameUser { get; set; } = null!;
        public string NameUser { get; set; } = null!;
        public string? PatronymicUser { get; set; }
        public string LoginUser { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;
        public int IdUserRole { get; set; }

        public virtual UserRole IdUserRoleNavigation { get; set; } = null!;
        public virtual ICollection<Executor> Executors { get; set; }
    }
}
