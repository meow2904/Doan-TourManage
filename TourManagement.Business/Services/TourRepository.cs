using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models;
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
            var tourOrderDetail = Context.OrderTourDetails.FirstOrDefault(x => x.TourId == tourId);
            var tour = Context.Tours.FirstOrDefault(x => x.Id == tourId);

            int remainQuantity = tour.QuantityPeople.Value;
            if (tourOrderDetail == null)
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

        public IEnumerable<Tour> GetToursByCategoryWithPaging(string category, int page, int size)
        {
            return Context.Tours.Where(x => x.Category.Name == category).
                OrderBy(x => x.PriceOfAdult).Skip(size * (page - 1)).Take(size).ToList();
        }

        public IEnumerable<Tour> GetToursByPriceWithPaging(string category, decimal startPrice, decimal endPrice, int page, int size)
        {
            var tours = Context.Tours.Where(x => x.Category.Name == category).ToList();
            return tours.Where(x => x.PriceOfAdult >= startPrice && x.PriceOfAdult <= endPrice).
                OrderBy(x => x.PriceOfAdult).Skip(size * (page - 1)).Take(size).ToList();
        }

        public IEnumerable<Tour> Search(string tour)
        {
            return Context.Tours.Where(x => x.Name.Contains(tour.ToLower()) || 
            x.TourDestinations.Any(y => y.Destination.Name.Contains(tour.ToLower())) ).ToList();
        }

        public IEnumerable<Tour> GetByPrice(string category, decimal startPrice, decimal endPrice)
        {
            var tours = Context.Tours.Where(x => x.Category.Name == category).ToList();
            return tours.Where(x => x.PriceOfAdult >= startPrice && x.PriceOfAdult <= endPrice).ToList();
        }

        public IEnumerable<Tour> GetToursByDateWithPaging(DateTime date, int page, int size)
        {
            return Context.Tours.Where(x => x.TimeStart.Value > date).
                OrderBy(x => x.Name).Skip(size * (page - 1)).Take(size).ToList();
        }

        public int CountTourByEmpId(int empId)
        {
            return Context.Tours.Where(x => x.EmployeeId == empId).Count();
        }

        public IEnumerable<Category> GetCategory()
        {
            return Context.Categories.ToList();
        }

        public IEnumerable<Tour> SearchByDate(DateTime dateTime)
        {
            var day = dateTime.AddDays(3);
            return Context.Tours.Where(x => x.TimeStart.Value.Day == day.Day &&
                    x.TimeStart.Value.Month == day.Month &&
                    x.TimeStart.Value.Year == day.Year
            );
        }
    }
}