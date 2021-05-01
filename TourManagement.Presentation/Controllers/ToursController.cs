using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;
using TourManagement.Models;
using TourManagement.Models.DBContext;

namespace TourManagement.Presentation.Controllers
{
    public class ToursController : Controller
    {
        //private TourManagementContext db = new TourManagementContext();
        private readonly ITourRepository _tourRepository;
        private readonly IDestinatioRepository _destinatioRepository;
        private int size = 8;
        public ToursController(ITourRepository tourRepository, IDestinatioRepository destinatioRepository)
        {
            _tourRepository = tourRepository;
            _destinatioRepository = destinatioRepository;
        }

        public ActionResult Details(int? id)
        {

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
            ViewBag.Destination = dess.Substring(0, dess.Length - 3);

            //get remain quantity in tour
            var remainingQuantity = _tourRepository.GetRemainingQuantity((int)id);
            ViewBag.Remaining = remainingQuantity;


            return View(tour);
        }

        public ActionResult GetByCategory(string category, int page)
        {
            int totalTourByCategory = _tourRepository.GetByCategory(category).Count();
            if (page <= 0)
            {
                page = 1;
            }

            var totalPage = (int)Math.Ceiling(totalTourByCategory / (double)size);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentCategory = category;

            var tours = _tourRepository.GetToursByCategoryWithPaging(category, page, size);
            return View(tours);
        }

        public ActionResult GetByPrice(string category, decimal startPrice, decimal endPrice, int page)
        {
            if (page < 0)
            {
                page = 1;
            }
            int totalTour = _tourRepository.GetByPrice(category, startPrice, endPrice).Count();
            var totalPage = (int)Math.Ceiling(totalTour / (double)size);
            ViewBag.TotalPage = totalPage;

            ViewBag.CurrentPage = page;
            ViewBag.CurrentCategory = category;

            var tours = _tourRepository.GetToursByPriceWithPaging(category, startPrice, endPrice, page, size);
            return View(tours);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
