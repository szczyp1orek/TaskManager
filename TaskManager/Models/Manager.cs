using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }

        public string Name { get; set; }

        public virtual List<Employee> Employee { get; set; }
        

    }
}