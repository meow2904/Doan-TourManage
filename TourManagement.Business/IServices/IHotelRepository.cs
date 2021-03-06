using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        IEnumerable<Hotel> GetHotelWithPaging(int page, int size);
    }
}
