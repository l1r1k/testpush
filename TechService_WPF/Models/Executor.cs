using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class Executor
    {
        public Executor()
        {
            ExecutorComments = new HashSet<ExecutorComment>();
        }

        public int IdExecutor { get; set; }
        public int IdUser { get; set; }
        public int IdRequest { get; set; }

        public virtual Request IdRequestNavigation { get; set; } = null!;
        public virtual UserService IdUserNavigation { get; set; } = null!;
        public virtual ICollection<ExecutorComment> ExecutorComments { get; set; }
    }
}
