﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TaskManager.Models;

namespace TaskManager.Models
{
    public class SampleData:DropCreateDatabaseAlways<TaskManagerEntities>
    {
        protected override void Seed(TaskManagerEntities context)
        {
            var managers = new List<Manager>
            {
                new Manager{Name="Jon Snow" },
                new Manager{Name="Daenerys Targaryen" }
            };
            var employees = new List<Employee>
            {
                new Employee{Name="Kamil Szcz", Manager=managers.Single(m=>m.Name.Contains("Jon Snow")) },
                new Employee{Name="Aga Nowak" ,  Manager=managers.Single(m=>m.Name.Contains("Jon Snow")) },
                new Employee{Name="Lord Varys" ,  Manager=managers.Single(m=>m.Name.Contains("Daenerys Targaryen")) },
                new Employee{Name="Tyrion Lennister" ,  Manager=managers.Single(m=>m.Name.Contains("Daenerys Targaryen")) },
            };
            var mtasks = new List<MTask>
            {
                new MTask{Name="Frontier Report", Description="just do it", Employee=employees.Single(m=>m.Name=="Kamil Szcz"), Date=new DateTime(2016,7,20), IsDone=false },
                new MTask{Name="EEC rates", Description="EEC rates upload", Employee=employees.Single(m=>m.Name=="Kamil Szcz"),Date=new DateTime(2016,7,20), IsDone=true },
                new MTask{Name="IBS-TOLAS recon", Description="compares balances on IBS and TOLAS accounts", Employee=employees.Single(m=>m.Name=="Aga Nowak"),Date=new DateTime(2016,7,20), IsDone=false }
            };
            foreach (var item in managers)
            {
                context.Managers.Add(item);
            }
            foreach (var item in employees)
            {
                context.Employees.Add(item);
            }
            foreach (var item in mtasks)
            {
                context.MTasks.Add(item);
            }
        }
    }
    
}