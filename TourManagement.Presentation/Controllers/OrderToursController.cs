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
        private readonly ITourRepository _tourRepository;
        private readonly IBookTourRepository _bookTourRepository;


        public OrderToursController(ITourRepository tourRepository,
            IBookTourRepository bookTourRepository)
        {
            _tourRepository = tourRepository;
            _bookTourRepository = bookTourRepository;
        }

        public ActionResult OrderTour(int id)
        {
            var tour = _tourRepository.GetById(id);
            ViewBag.Tour = tour;

            var remainingQuantity = _tourRepository.GetRemainingQuantity((int)id);
            ViewBag.Remaining = remainingQuantity;

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTour(OrderTourDetail orderTourDetail)
        {
            //get custommer from session
            var cusInfor = (User)Session["username"];
            if (ModelState.IsValid)
            {
                var orderTour = new OrderTour();
                orderTour.UserId = cusInfor.Id;

                _bookTourRepository.BookTour(orderTour, orderTourDetail);
                return RedirectToAction("CompleteOrder");
            }

            
            return View(orderTourDetail);
        }

        public ActionResult CompleteOrder()
        {
            ViewBag.CompleteMessage = "Cảm ơn bạn đã đặt tour, đơn hàng của bạn đang được xử lý";

            return View();
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
