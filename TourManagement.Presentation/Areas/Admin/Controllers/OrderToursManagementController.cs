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

        public OrderToursManagementController(IOrderTourRepository orderTourRepository,
            IOrderTourDetailRepository orderTourDetailRepository)
        {
            _orderTourRepository = orderTourRepository;
            _orderTourDetailRepository = orderTourDetailRepository;
        }

        //take ordertour with user of tour is unconditional
        public ActionResult GetOrdersTour(int tourId)
        {
            var ordersTour = _orderTourRepository.GetOrderTourByTour(tourId);
            return View(ordersTour);
        }

        // delete ordertour with user of tour is unconditional
        [HttpPost]
        public ActionResult Delete(int orderTourId, int tourId)
        {
            var orderTourDetail = _orderTourDetailRepository.GetOrderTourDetailByOrderId(orderTourId);
            _orderTourDetailRepository.Delete(orderTourDetail);
            _orderTourRepository.DeleteByID(orderTourId);
            return RedirectToAction("GetOrdersTour", new { tourId = tourId });
        }

        //view order with status pending and confirmed
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult GetOrders(string status)
        {
            var listOrderStatus = _orderTourRepository.GetOrderTourByStatus(status);
            
            return PartialView(listOrderStatus);
        }
        
        public ActionResult GetInfor(int orderTourId)
        {
            var infor = _orderTourRepository.GetById(orderTourId);
            return PartialView(infor);
        }

        public ActionResult UpdateOrders(int orderTourId, string status)
        {
            var orderTour = _orderTourRepository.GetById(orderTourId);
            orderTour.Status = status;
            _orderTourRepository.Update(orderTour);

            return PartialView();
        }
    }
}

