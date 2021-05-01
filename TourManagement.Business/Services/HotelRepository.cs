using System.Collections.Generic;
using System.Linq;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public IEnumerable<Hotel> GetHotelWithPaging(int page, int size)
        {
            return Context.Hotels.OrderBy(x => x.Name).Skip(size * (page - 1)).Take(size).ToList();
        }
    }
}
