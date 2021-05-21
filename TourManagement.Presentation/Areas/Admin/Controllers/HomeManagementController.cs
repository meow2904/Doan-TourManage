using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class HomeManagementController : Controller
    {
        private readonly IOrderTourRepository _orderTourRepository;
        private readonly IOrderTourDetailRepository _orderTourDetailRepository;
        private readonly ITourRepository _tourRepository;
        public HomeManagementController(IOrderTourRepository orderTourRepository,
            IOrderTourDetailRepository orderTourDetailRepository,
            ITourRepository tourRepository)
        {
            _orderTourRepository = orderTourRepository;
            _orderTourDetailRepository = orderTourDetailRepository;
            _tourRepository = tourRepository;
        }

        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                ViewBag.orderPending = _orderTourRepository.GetOrderTourByStatus("Pending").ToList().Count;
                ViewBag.orderCancel = _orderTourRepository.GetOrderTourByStatus("Cancel").ToList().Count;
                ViewBag.orderConfirmed = _orderTourRepository.GetOrderTourByStatus("Confirmed").ToList().Count;
                ViewBag.total = _orderTourRepository.GetAll().ToList().Count;


                return View();
            }

        }

        public JsonResult GetOrdersByMonth(DateTime fromDate, DateTime toDate)
        {
            var dt = fromDate.Date;
            var listOrders = _orderTourRepository.GetAll().Where(x => x.Status == "Confirmed" &&
                                                                x.OrderDate >= fromDate &&
                                                                x.OrderDate <= toDate &&
                                                                x.OrderTourDetails.Any(od=> od.Tour.TimeStart <= toDate)).ToList();

            return Json(new
            {
                ListOrders = JsonConvert.SerializeObject(listOrders),
            }, JsonRequestBehavior.AllowGet);
        }


    }
}