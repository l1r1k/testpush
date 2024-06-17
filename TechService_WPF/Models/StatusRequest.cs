using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class StatusRequest
    {
        public StatusRequest()
        {
            Requests = new HashSet<Request>();
        }

        public int IdStatusRequest { get; set; }
        public string NameStatusRequest { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
