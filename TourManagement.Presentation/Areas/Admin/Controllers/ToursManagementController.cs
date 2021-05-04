using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        private readonly IDestinatioRepository _destinatioRepository;
        private readonly ITourDestinationRepository _tourDestinationRepository;
        public ToursManagementController(ITourRepository tourRepository,
            IEmployeeRepository employeeRepository,
            IDestinatioRepository destinatioRepository,
            ITourDestinationRepository tourDestinationRepository)
        {
            _tourRepository = tourRepository;
            _employeeRepository = employeeRepository;
            _destinatioRepository = destinatioRepository;
            _tourDestinationRepository = tourDestinationRepository;
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
            ViewBag.CategoryId = new SelectList(_tourRepository.GetCategory(), "Id", "Name");
            ViewBag.DestiantionId = new SelectList(_destinatioRepository.GetAll(), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(_employeeRepository.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Admin/ToursManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tour tour, List<string> DestinationIds, List<HttpPostedFileBase> filesInput)
        {
            if (ModelState.IsValid)
            {

                if (filesInput != null)
                {
                    try
                    {
                        foreach(var item in filesInput)
                        {
                            string fileName = "";
                            fileName = Path.GetFileName(item.FileName);
                            string path = Path.Combine(Server.MapPath("~/Content/images/tours"), fileName);
                            item.SaveAs(path);
                        }
                        
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                _tourRepository.Add(tour);
                foreach (var item in DestinationIds)
                {
                    
                    var tourdestination = new TourDestination();
                    tourdestination.IdTour = tour.Id;
                    tourdestination.IdDestination = Int32.Parse(item);

                    _tourDestinationRepository.Add(tourdestination);
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", tour.CategoryId);
            
            return View(tour);
        }

        [HttpGet]
        public ActionResult GetEmployee(DateTime datePick, int time)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var employees = _employeeRepository.GetEmployeeFree(datePick, time);
            //var json = new JavaScriptSerializer().Serialize(employees.ToList());

            //ViewBag.Employees = new SelectList(employees.OrderBy(x => x.Name), "EmployeeID", "Name");

            string empNull = "";
            foreach (var item in employees)
            {
                empNull += item.Id + "," + item.Name + "-";
            }
            return Content(empNull);
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
