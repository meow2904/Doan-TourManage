using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface IOrderTourRepository: IGenericRepository<OrderTour>
    {
        IEnumerable<OrderTour> GetOrderTourByCustommer(int cusId);
        IEnumerable<OrderTour> GetOrderTourByTour(int tourId);
        IEnumerable<OrderTour> GetOrderTourByStatus(string status);

        IEnumerable<OrderTour> GetOrderDone();
    }
}
