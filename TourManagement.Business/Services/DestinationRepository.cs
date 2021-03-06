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
    public class DestinationRepository : GenericRepository<Destination>, IDestinatioRepository
    {
        public IEnumerable<Destination> GetDestinationByidTour(int idTour)
        {

            return Context.Destinations.Where(x => x.TourDestinations.Any( td => td.IdTour == idTour)).ToList();
        }

    }
}
