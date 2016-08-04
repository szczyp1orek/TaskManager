using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult Index()
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var tasks = taskDB.MTasks.ToList();

            return View(tasks);
        }
        
        // GET: Task
        public ActionResult Details(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var task = taskDB.MTasks.Find(id);         
            return View(task);
        }
        //[Authorize(Roles = "Admin")] 
        public ActionResult Create()
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            ViewBag.EmployeeId = new SelectList(taskDB.Employees,"EmployeeId","Name");
            return View();
        }
        //[Authorize(Roles = "Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MTask task)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            if (ModelState.IsValid)
            {
                taskDB.MTasks.Add(task);
                taskDB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.EmployeeId = new SelectList(taskDB.Employees, "EmployeeId", "Surname");
            return View(task);
        }
        //[Authorize(Roles = "Admin")]
        
        public ActionResult Edit(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var task = taskDB.MTasks.Find(id);
            ViewBag.EmployeeId = new SelectList(taskDB.Employees, "EmployeeId", "Name");
            return View(task);
        }
        //[Authorize(Roles = "Admin")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MTask task)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            if (ModelState.IsValid)
            {
                taskDB.Entry(task).State = EntityState.Modified;
                taskDB.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.EmployeeId = new SelectList(taskDB.Employees, "EmployeeId", "Name");
            return View(task);
        }
        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var taskToRemove = taskDB.MTasks.Find(id);
            return View(taskToRemove);
        }
        //[Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var task=taskDB.MTasks.Find(id);
            taskDB.MTasks.Remove(task);
            taskDB.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}