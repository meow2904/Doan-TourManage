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
        private readonly ITourRepository _tourRepository;
        private readonly IBookTourRepository _bookTourRepository;
        private readonly IOrderTourRepository _orderTourRepository;


        public OrderToursController(ITourRepository tourRepository,
            IBookTourRepository bookTourRepository,
            IOrderTourRepository orderTourRepository)
        {
            _tourRepository = tourRepository;
            _bookTourRepository = bookTourRepository;
            _orderTourRepository = orderTourRepository;
        }

        public ActionResult GetMyOrders()
        {
            var custommer = (User)Session["username"];
            var orderTour = _orderTourRepository.GetOrderTourByCustommer(custommer.Id);
            return View(orderTour);
        }

        public ActionResult OrderTour(int id)
        {
            var tour = _tourRepository.GetById(id);
            ViewBag.Tour = tour;
            string[] listImages = tour.Image.Split('-');
            ViewBag.ListImages = listImages;

            var remainingQuantity = _tourRepository.GetRemainingQuantity((int)id);
            ViewBag.Remaining = remainingQuantity;

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }    
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderTour(OrderTourDetail orderTourDetail, decimal SumPrice)
        {
            //get custommer from session
            var cusInfor = (User)Session["username"];
            if (ModelState.IsValid)
            {
                var orderTour = new OrderTour();
                orderTour.SumPrice = SumPrice;
                orderTour.UserId = cusInfor.Id;
                orderTour.Status = "Pending";
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

        public ActionResult GetInforOrder(int orderId)
        {
            var order = _orderTourRepository.GetById(orderId);
            return PartialView(order);
        }
    }
}
