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
    public class TourDestinationRepository : GenericRepository<TourDestination>, ITourDestinationRepository
    {
        public IEnumerable<TourDestination> GetListTourDesination(int tourId)
        {
            return Context.TourDestinations.Where(x => x.IdTour == tourId).ToList();
        }
    }
}
