using System.Collections.Generic;
using System.Linq;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public IEnumerable<Room> GetRoomByHotelId(int hotelId)
        {
            return Context.Rooms.Where(x => x.HotelId == hotelId).ToList();
        }
    }
}
