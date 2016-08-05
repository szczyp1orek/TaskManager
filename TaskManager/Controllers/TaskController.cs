using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var tasks = taskDB.MTasks.ToList();

            return View(tasks);
        }
        
        // GET: Task
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var task = taskDB.MTasks.Find(id);         
            return View(task);
        }
        
        public ActionResult Create()
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            ViewBag.EmployeeId = new SelectList(taskDB.Employees,"EmployeeId","Name");
            return View();
        }
        
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
        
        
        public ActionResult Edit(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var task = taskDB.MTasks.Find(id);
            ViewBag.EmployeeId = new SelectList(taskDB.Employees, "EmployeeId", "Name");
            return View(task);
        }
        
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
        
        public ActionResult Delete(int id)
        {
            TaskManagerEntities taskDB = new TaskManagerEntities();
            var taskToRemove = taskDB.MTasks.Find(id);
            return View(taskToRemove);
        }
        
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