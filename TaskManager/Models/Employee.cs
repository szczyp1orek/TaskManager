using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace TaskManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [DisplayName("Employee")]
        public string Name { get; set; }
      
        public virtual Manager Manager { get; set; }
        //public virtual Department Department { get; set; }
        public virtual List<MTask> MTasks { get; set; }
    }
}   