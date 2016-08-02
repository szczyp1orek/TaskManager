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
        TaskManagerEntities taskDB = new TaskManagerEntities();
        // GET: Task
        public ActionResult Details(int id)
        {
            var task = taskDB.MTasks.Find(id);         
            return View(task);
        }
        //[Authorize(Roles = "Admins")] 
        public ActionResult Create()
        {
            
            ViewBag.EmployeeId = new SelectList(taskDB.Employees,"EmployeeId","Name");
            return View();
        }
        //[Authorize(Roles = "Admins")] 
        [HttpPost]
        public ActionResult Create(MTask task)
        {
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
            var task = taskDB.MTasks.Find(id);
            ViewBag.EmployeeId = new SelectList(taskDB.Employees, "EmployeeId", "Name");
            return View(task);
        }
        //[Authorize(Roles = "Admins")] 
        [HttpPost]
        public ActionResult Edit(MTask task)
        {
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
            var taskToRemove = taskDB.MTasks.Find(id);
            return View(taskToRemove);
        }
        //[Authorize(Roles = "Admins")] 
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var task=taskDB.MTasks.Find(id);
            taskDB.MTasks.Remove(task);
            taskDB.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}