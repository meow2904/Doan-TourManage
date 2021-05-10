using System.Collections.Generic;
using System.Linq;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class OrderTourRepository : GenericRepository<OrderTour>, IOrderTourRepository
    {
        public IEnumerable<OrderTour> GetOrderTourByCustommer(int cusId)
        {
            return Context.OrderTours.Where(x => x.UserId == cusId).ToList();
        }

        public IEnumerable<OrderTour> GetOrderTourByTour(int tourId)
        {
            return Context.OrderTours.Where(x => x.OrderTourDetails.Any(y => y.TourId == tourId)).ToList();
        }
    }
}
