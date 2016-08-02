using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            //var tEdit = new MTasksEdit();
            var tasks = taskDB.MTasks.Where(t => t.IsDone == false).ToList();
            //tEdit.tasksEdit = tasks;
            return View(tasks);
        }
        ///
        [HttpPost]
        public ActionResult Index(List<MTask> tasks)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            
            if (ModelState.IsValid)
            {
                foreach (var item in tasks)
                {
                    taskDB.Entry(item).State = EntityState.Modified;
                }
                taskDB.SaveChanges();
                return RedirectToAction("Index");
            }

            tasks = taskDB.MTasks.Where(t => t.IsDone == false).ToList();
            return View(tasks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "My Task Manager";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "lilek8@gmail.com";

            return View();
        }
    }
}