using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeeFree(DateTime datePick, int time);
        IEnumerable<Employee> GetEmployeesWithPaging(int page, int size);
    }
}
