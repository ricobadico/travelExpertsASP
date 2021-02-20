using System;
using System.Collections.Generic;

namespace TravelExperts.Repository.Domain
{
    public partial class Agents
    {
        public Agents()
        {
            Customers = new HashSet<Customer>();
        }

        public int AgentId { get; set; }
        public string AgtFirstName { get; set; }
        public string AgtMiddleInitial { get; set; }
        public string AgtLastName { get; set; }
        public string AgtBusPhone { get; set; }
        public string AgtEmail { get; set; }
        public string AgtPosition { get; set; }
        public int? AgencyId { get; set; }

        public virtual Agencies Agency { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
