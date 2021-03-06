using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class OrderTourDetailRepository : GenericRepository<OrderTourDetail>, IOrderTourDetailRepository
    {
        public OrderTourDetail GetOrderTourDetailByOrderId(int orderId)
        {
            return Context.OrderTourDetails.FirstOrDefault(x => x.OrderId == orderId);
        }
    }
}
