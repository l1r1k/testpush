using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class Request
    {
        public Request()
        {
            Executors = new HashSet<Executor>();
        }

        public int IdRequest { get; set; }
        public string NumberRequest { get; set; } = null!;
        public DateTime? DateAddingRequest { get; set; }
        public DateTime? DateClosingRequest { get; set; }
        public int IdEquipment { get; set; }
        public int IdTypeErrorSupply { get; set; }
        public string InfoAboutProblemRequest { get; set; } = null!;
        public int IdClient { get; set; }
        public int IdStatusRequest { get; set; }

        public virtual Client IdClientNavigation { get; set; } = null!;
        public virtual Equipment IdEquipmentNavigation { get; set; } = null!;
        public virtual StatusRequest IdStatusRequestNavigation { get; set; } = null!;
        public virtual TypeErrorSupply IdTypeErrorSupplyNavigation { get; set; } = null!;
        public virtual ICollection<Executor> Executors { get; set; }
    }
}
