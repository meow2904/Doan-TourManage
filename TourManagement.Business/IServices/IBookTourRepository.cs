using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface IBookTourRepository
    {
        void BookTour(OrderTour orderTour, OrderTourDetail orderTourDetail);
    }
}
