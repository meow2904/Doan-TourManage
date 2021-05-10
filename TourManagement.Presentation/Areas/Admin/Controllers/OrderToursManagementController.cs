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

        public ActionResult GetOrdersTour(int tourId)
        {
            var ordersTour = _orderTourRepository.GetOrderTourByTour(tourId);
            return View(ordersTour);
        }

        [HttpPost]
        public ActionResult Delete(int orderTourId, int tourId)
        {
            var orderTourDetail = _orderTourDetailRepository.GetOrderTourDetailByOrderId(orderTourId);
            _orderTourDetailRepository.Delete(orderTourDetail);
            _orderTourRepository.DeleteByID(orderTourId);
            return RedirectToAction("GetOrdersTour", new { tourId = tourId });
        }
       
    }
}
