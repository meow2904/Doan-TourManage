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

        public ActionResult CheckTour()
        {
            var tours = _tourRepository.SearchByDate(DateTime.Now);
            List<Tour> listTourCancel = new List<Tour>();
            foreach(var item in tours)
            {
                int a = _tourRepository.GetRemainingQuantity(item.Id);
                if (a > 10)
                {
                    listTourCancel.Add(item);
                }
            }

            return View(listTourCancel);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Tour tour, List<string> DestinationIds, List<HttpPostedFileBase> filesInput)
        {
            if (ModelState.IsValid)
            {

                if (filesInput != null)
                {
                    try
                    {
                        foreach (var item in filesInput)
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
            ViewBag.DestiantionId = new SelectList(_destinatioRepository.GetAll(), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(_employeeRepository.GetAll(), "Id", "Name");
            return View(tour);
        }


        [HttpGet]
        public ActionResult GetEmployee(DateTime datePick, int time)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var employees = _employeeRepository.GetEmployeeFree(datePick, time);

            ViewBag.Employees = new SelectList(employees.OrderBy(x => x.Name), "EmployeeID", "Name");

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
            var tour = _tourRepository.GetById((int)id);

            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", tour.CategoryId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", tour.EmployeeId);
            ViewBag.DestiantionId = new SelectList(_destinatioRepository.GetAll(), "Id", "Name");

            return View(tour);
        }


        // POST: Admin/ToursManagement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Tour tour, List<string> DestinationIds, List<HttpPostedFileBase> filesInput)
        {
            if (ModelState.IsValid)
            {
                if (filesInput != null)
                {
                    try
                    {
                        foreach (var item in filesInput)
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

                _tourRepository.Update(tour);
                //delete old tourdes 
                var tds = _tourDestinationRepository.GetListTourDesination(tour.Id);
                foreach (var item in tds)
                {
                    _tourDestinationRepository.Delete(item);
                }

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
            ViewBag.DestiantionId = new SelectList(_destinatioRepository.GetAll(), "Id", "Name");
            ViewBag.EmployeeId = new SelectList(_employeeRepository.GetAll(), "Id", "Name");
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
