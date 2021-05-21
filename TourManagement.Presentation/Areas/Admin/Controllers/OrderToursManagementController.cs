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
using Newtonsoft.Json;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class OrderToursManagementController : Controller
    {
        private readonly IOrderTourRepository _orderTourRepository;
        private readonly IOrderTourDetailRepository _orderTourDetailRepository;
        private readonly ITourRepository _tourRepository;

        public OrderToursManagementController(IOrderTourRepository orderTourRepository,
            IOrderTourDetailRepository orderTourDetailRepository,
            ITourRepository tourRepository
            )
        {
            _orderTourRepository = orderTourRepository;
            _orderTourDetailRepository = orderTourDetailRepository;
            _tourRepository = tourRepository;
        }

        //take ordertour with user of tour is unconditional
        public ActionResult GetOrdersTour(int tourId)
        {
            var ordersByTourId = _orderTourRepository.GetOrderTourByTour(tourId);
            var order = ordersByTourId.Where(x => x.Status != "Cancel").ToList();

            return View(order);
        }

        // delete ordertour with user of tour is unconditional
        [HttpPost]
        public ActionResult CancelOrder(int orderTourId, int tourId)
        {
            var order = _orderTourRepository.GetById(orderTourId);
            var orderDetail = order.OrderTourDetails;
            var tour = orderDetail.First().Tour;

            order.Status = "Cancel";
            var quanOrder = orderDetail.First().QuantityChild + orderDetail.First().QuantityAdult;
            tour.QuantityPeople += quanOrder;

            _orderTourRepository.Update(order);
            return RedirectToAction("GetOrdersTour", new { tourId = tourId });
        }

        //view order with status pending and confirmed
        public ActionResult Orders()
        {
            return View();
        }

        public JsonResult GetOrders()
        {
            var listPending = _orderTourRepository.GetOrderTourByStatus("Pending");
            var listConfirmed = _orderTourRepository.GetOrderTourByStatus("Confirmed").OrderByDescending(x => x.OrderDate);
            return Json(new
            {
                ListPending = JsonConvert.SerializeObject(listPending),
                ListConfirmed = JsonConvert.SerializeObject(listConfirmed),
            }, JsonRequestBehavior.AllowGet);

            //if (status == "Confirmed")
            //{
            //   var listOrderStatus = _orderTourRepository.GetOrderTourByStatus(status).OrderByDescending(x => x.OrderDate);
            //   return PartialView(listOrderStatus);
            //}
            //else
            //{
            //    var listOrderStatus = _orderTourRepository.GetOrderTourByStatus(status);
            //    return PartialView(listOrderStatus);
            //}

        }
        
        public ActionResult GetInformationOrder(int orderTourId)
        {
            var infor = _orderTourRepository.GetById(orderTourId);
            return PartialView(infor);
        }

        public ActionResult UpdateOrders(int orderTourId, string status)
        {
            var orderTour = _orderTourRepository.GetById(orderTourId);
            var orderTourDetail = orderTour.OrderTourDetails;
            var tour = orderTourDetail.First().Tour;
            if (status == "Confirmed")
            {
                orderTour.Status = status;
            }
            else
            {
                orderTour.Status = status;

                var quanOrder = orderTourDetail.First().QuantityChild + orderTourDetail.First().QuantityAdult;
                tour.QuantityPeople += quanOrder;
                _tourRepository.Update(tour);
            }
            //update quantity
            _orderTourRepository.Update(orderTour);
            return Content("Thành công");
        }
    }
}

