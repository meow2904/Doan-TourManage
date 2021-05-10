using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface ITourRepository : IGenericRepository<Tour>
    {
        IEnumerable<Tour> GetByCategory(string category);
        int GetRemainingQuantity(int tourId);
        IEnumerable<Tour> Search(string tour);
        IEnumerable<Tour> GetToursByCategoryWithPaging(string category, int page, int size);
        IEnumerable<Tour> GetToursByDateWithPaging(DateTime date, int page, int size);
        IEnumerable<Tour> GetByPrice(string category, decimal startPrice, decimal endPrice);
        IEnumerable<Tour> GetToursByPriceWithPaging(string category, decimal startPrice, decimal endPrice, int page, int size);
        int CountTourByEmpId(int empId);
        IEnumerable<Category> GetCategory();
        IEnumerable<Tour> SearchByDate(DateTime dateTime);


    }
}
