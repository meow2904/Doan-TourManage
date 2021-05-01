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
using TourManagement.Business.Services;

namespace TourManagement.Presentation.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelRepository _hotelRepository;
        private int size = 9;
        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public ActionResult Index(int page)
        {
            var totalHotels = _hotelRepository.GetAll().Count();
            if(page <= 0)
            {
                page = 1;
            }
            var totalPage = (int)Math.Ceiling(totalHotels / (double)size);
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = page;

            var hotels = _hotelRepository.GetHotelWithPaging(page, size);
            return View(hotels);
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hotel = _hotelRepository.GetById((int)id);
            return View(hotel);
        }

        public ActionResult ConfirmContact()
        {
            var custommer = (User)Session["username"];
            if(custommer == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }

    }
}
