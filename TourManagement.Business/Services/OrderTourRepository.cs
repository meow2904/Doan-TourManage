using System;
using System.Collections.Generic;
using System.Linq;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class OrderTourRepository : GenericRepository<OrderTour>, IOrderTourRepository
    {
        public IEnumerable<OrderTour> GetOrderDone()
        {
            return Context.OrderTours.Where(x => x.OrderTourDetails.Any(od => od.Tour.TimeStart <= DateTime.Now)).ToList();
        }

        public IEnumerable<OrderTour> GetOrderTourByCustommer(int cusId)
        {
            return Context.OrderTours.Where(x => x.UserId == cusId).ToList();
        }

        public IEnumerable<OrderTour> GetOrderTourByStatus(string status)
        {
            return Context.OrderTours.Where(x => x.Status == status).ToList();
        }

        public IEnumerable<OrderTour> GetOrderTourByTour(int tourId)
        {
            return Context.OrderTours.Where(x => x.OrderTourDetails.Any(y => y.TourId == tourId)).ToList();
        }
    }
}
