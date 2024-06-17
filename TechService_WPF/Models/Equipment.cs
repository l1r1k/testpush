using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Requests = new HashSet<Request>();
        }

        public int IdEquipment { get; set; }
        public string NameEquipment { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
