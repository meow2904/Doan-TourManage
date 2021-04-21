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
    public class TourRepository : GenericRepository<Tour>, ITourRepository
    {
        public IEnumerable<Tour> GetByCategory(string category)
        {
            return Context.Tours.Where(x => x.Category.Name == category).ToList();
        }

        public int GetRemainingQuantity(int tourId)
        {
            var quantity1 = Context.OrderTourDetails.Where(o => o.TourId == tourId).Sum(o => o.QuantityAdult);
            var quantity2 = Context.OrderTourDetails.Where(o => o.TourId == tourId).Sum(o => o.QuantityChild);
            var remainQuantity = Context.Tours.Where(x => x.Id == tourId).Sum(x => x.QuantityPeople) - (quantity1 + quantity2);

            return (int)remainQuantity;
        }

        public IEnumerable<Tour> Search(string tour)
        {
            return Context.Tours.Where(x => x.Name.Contains(tour)).ToList();
        }
    }
}