using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class ToursManagementController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private readonly ITourRepository _tourRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ToursManagementController(ITourRepository tourRepository, IEmployeeRepository employeeRepository)
        {
            _tourRepository = tourRepository;
            _employeeRepository = employeeRepository;
        }
        // GET: Admin/ToursManagement
        public ActionResult Index()
        {
            var tours = _tourRepository.GetAll();
            return View(tours);
        }

        // GET: Admin/ToursManagement/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name");
            return View();
        }

        // POST: Admin/ToursManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Time,TimeStart,PositionStart,Transport,Content,Image,QuantityPeople,PriceOfChild,PriceOfAdult,EmployeeId,HotelId,CategoryId")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", tour.CategoryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", tour.EmployeeId);
            return View(tour);
        }

        // GET: Admin/ToursManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", tour.CategoryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", tour.EmployeeId);
            return View(tour);
        }

        // POST: Admin/ToursManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Time,TimeStart,PositionStart,Transport,Content,Image,QuantityPeople,PriceOfChild,PriceOfAdult,EmployeeId,HotelId,CategoryId")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", tour.CategoryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", tour.EmployeeId);
            return View(tour);
        }

        // GET: Admin/ToursManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            _tourRepository.DeleteByID((int)id);
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
