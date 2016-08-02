using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace TaskManager.Models
{
    
    public class MTask//zmienic nazwe do wyswietlenia na "Task"
    {
        public int MTaskId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Due date")]
        public DateTime Date { get; set; }
        [DisplayName("Is done")]
        public bool IsDone { get; set; }

        public virtual Employee Employee { get; set; }
        
    }
    /*
    public class MTasksEdit
    {
        public IEnumerable<MTask> tasksEdit { get; set; }
        public MTasksEdit()
        {
            if (tasksEdit == null)
                tasksEdit = new List<MTask>();
        }
    }*/
}