using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
    }
}
