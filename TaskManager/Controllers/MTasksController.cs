using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class MTasksController : Controller
    {
        private TaskManagerEntities db = new TaskManagerEntities();

        // GET: MTasks
        public ActionResult Index()
        {
            var mTasks = db.MTasks.Include(m => m.Employee);
            return View(mTasks.ToList());
        }

        // GET: MTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MTask mTask = db.MTasks.Find(id);
            if (mTask == null)
            {
                return HttpNotFound();
            }
            return View(mTask);
        }

        // GET: MTasks/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name");
            return View();
        }

        // POST: MTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MTaskId,Name,Description,Date,IsDone,EmployeeId")] MTask mTask)
        {
            if (ModelState.IsValid)
            {
                db.MTasks.Add(mTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", mTask.EmployeeId);
            return View(mTask);
        }

        // GET: MTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MTask mTask = db.MTasks.Find(id);
            if (mTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", mTask.EmployeeId);
            return View(mTask);
        }

        // POST: MTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MTaskId,Name,Description,Date,IsDone,EmployeeId")] MTask mTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", mTask.EmployeeId);
            return View(mTask);
        }

        // GET: MTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MTask mTask = db.MTasks.Find(id);
            if (mTask == null)
            {
                return HttpNotFound();
            }
            return View(mTask);
        }

        // POST: MTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MTask mTask = db.MTasks.Find(id);
            db.MTasks.Remove(mTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
