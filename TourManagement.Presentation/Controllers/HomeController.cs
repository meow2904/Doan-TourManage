using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;

namespace TourManagement.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourRepository _tourRepository;
        //private readonly IDestinatioRepository _destinatioRepository;


        public HomeController(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }
        public ActionResult Index()
        {
            var tours = _tourRepository.GetAll();
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