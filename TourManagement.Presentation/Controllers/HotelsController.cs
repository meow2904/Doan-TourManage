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
        private TourManagementContext db = new TourManagementContext();
        private readonly IHotelRepository _hotelRepository;
        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }
        public ActionResult Index()
        {
            var hotels = _hotelRepository.GetAll();
            return View(hotels);
        }

        // GET: Hotels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
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
