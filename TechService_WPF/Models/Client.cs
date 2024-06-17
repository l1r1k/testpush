using System;
using System.Collections.Generic;

namespace TechService_WPF.Models
{
    public partial class Client
    {
        public Client()
        {
            Requests = new HashSet<Request>();
        }

        public int IdClient { get; set; }
        public string SurnameClient { get; set; } = null!;
        public string NameClient { get; set; } = null!;
        public string PatronymicClient { get; set; } = null!;
        public string PhoneNumberClient { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
