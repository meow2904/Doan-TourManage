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

namespace TourManagement.Presentation.Controllers
{
    public class ToursController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private readonly ITourRepository _tourRepository;
        private readonly IDestinatioRepository _destinatioRepository;
        public ToursController(ITourRepository tourRepository, IDestinatioRepository destinatioRepository)
        {
            _tourRepository = tourRepository;
            _destinatioRepository = destinatioRepository;
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tour = _tourRepository.GetById((int)id);
            if (tour == null)
            {
                return HttpNotFound();
            }

            //get destiantions in tour
            var destinations = _destinatioRepository.GetDestinationByidTour((int)id);
            string dess = "";
            foreach (var item in destinations)
            {
                dess += item.Name + " - ";
            }

            //get remain quantity in tour
            var remainingQuantity = _tourRepository.GetRemainingQuantity((int)id);
            ViewBag.Remaining = remainingQuantity;
            ViewBag.Destination = dess.Substring(0, dess.Length - 3);
            return View(tour);
        }

        public ActionResult GetByCategory(string category)
        {
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tours = _tourRepository.GetByCategory(category);
            if (tours == null)
            {
                ViewBag.Message = "Don't have tour in foeign country";
            }
            return View(tours);
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
