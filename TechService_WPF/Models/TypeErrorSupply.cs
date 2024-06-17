using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class TypeErrorSupply
    {
        public TypeErrorSupply()
        {
            Requests = new HashSet<Request>();
        }

        public int IdTypeErrorSupply { get; set; }
        public string NameTypeErrorSupply { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
