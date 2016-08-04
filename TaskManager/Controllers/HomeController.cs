using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            var tasks = taskDB.MTasks.Where(t => t.IsDone == false).ToList();
            
            return View(tasks);
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<MTask> tasks)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            //var oldTasks = taskDB.MTasks.Where(t => t.IsDone == false);

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
            ViewBag.Message = "wiluszkamil1@gmail.com";

            return View();
        }
    }
}