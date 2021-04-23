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
            var tourOrderDetail = Context.OrderTourDetails.FirstOrDefault(x => x.TourId==tourId);
            var tour = Context.Tours.FirstOrDefault(x => x.Id==tourId);

            int remainQuantity=tour.QuantityPeople.Value;
            if(tourOrderDetail ==null)
            {
                return remainQuantity;
            }
            else
            {
                var quantity1 = Context.OrderTourDetails.Where(o => o.TourId == tourId).Sum(o => o.QuantityAdult);
                var quantity2 = Context.OrderTourDetails.Where(o => o.TourId == tourId).Sum(o => o.QuantityChild);
                remainQuantity = (int)(Context.Tours.Where(x => x.Id == tourId).Sum(x => x.QuantityPeople) - (quantity1 + quantity2));
                return remainQuantity;
            }        
        }

        public IEnumerable<Tour> Search(string tour)
        {
            return Context.Tours.Where(x => x.Name.Contains(tour.ToLower()) ||
            x.TourDestinations.Any(y => y.Destination.Name.Contains(tour.ToLower()))).ToList();
        }
    }
}