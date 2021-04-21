using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Models.DBContext;
using TourManagement.Business.IServices;


namespace TourManagement.Presentation.Controllers
{
    public class OrderToursController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        

        // GET: OrderTours
        public ActionResult Index()
        {
            var orderTours = db.OrderTours.Include(o => o.User);
            return View(orderTours.ToList());
        }

        // GET: OrderTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTour orderTour = db.OrderTours.Find(id);
            if (orderTour == null)
            {
                return HttpNotFound();
            }
            return View(orderTour);
        }

        // GET: OrderTours/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: OrderTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UserId,OrderDate")] OrderTour orderTour)
        {
            if (ModelState.IsValid)
            {
                db.OrderTours.Add(orderTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", orderTour.UserId);
            return View(orderTour);
        }

        // GET: OrderTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTour orderTour = db.OrderTours.Find(id);
            if (orderTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", orderTour.UserId);
            return View(orderTour);
        }

        // POST: OrderTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,OrderDate")] OrderTour orderTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", orderTour.UserId);
            return View(orderTour);
        }

        // GET: OrderTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTour orderTour = db.OrderTours.Find(id);
            if (orderTour == null)
            {
                return HttpNotFound();
            }
            return View(orderTour);
        }

        // POST: OrderTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTour orderTour = db.OrderTours.Find(id);
            db.OrderTours.Remove(orderTour);
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
