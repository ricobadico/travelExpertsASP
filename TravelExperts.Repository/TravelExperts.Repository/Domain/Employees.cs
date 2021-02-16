using System;
using System.Collections.Generic;

namespace TravelExperts.Repository.Domain
{
    public partial class Employees
    {
        public string EmpFirstName { get; set; }
        public string EmpMiddleInitial { get; set; }
        public string EmpLastName { get; set; }
        public string EmpBusPhone { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPosition { get; set; }
    }
}
