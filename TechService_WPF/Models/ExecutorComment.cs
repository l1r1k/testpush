using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class ExecutorComment
    {
        public int IdExecutorComment { get; set; }
        public string CommentExecutorComment { get; set; } = null!;
        public int IdExecutor { get; set; }

        public virtual Executor IdExecutorNavigation { get; set; } = null!;
    }
}
