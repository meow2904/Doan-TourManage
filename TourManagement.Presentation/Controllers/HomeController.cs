using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;

namespace TourManagement.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourRepository _tourRepository;
        private int size = 8;

        public HomeController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }
        public ActionResult Index()
        {
            var date = DateTime.Now.Date;
            var tours = _tourRepository.GetToursByDateWithPaging(date, 1, size);
            return View(tours);
        }


        public ActionResult SearchTour(string search)
        {
            var toursSearch = _tourRepository.Search(search);
            ViewBag.MessageSearch = search;
            return View(toursSearch);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}