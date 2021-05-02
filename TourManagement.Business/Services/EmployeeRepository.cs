using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.Services
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        [Obsolete]
        public IEnumerable<Employee> GetEmployeeFree(DateTime datePick, int time)
        {
            var emps = Context.Tours.Where(t => (t.TimeStart >= datePick && t.TimeStart <= EntityFunctions.AddDays(datePick, time)) ||
                                                EntityFunctions.AddDays(t.TimeStart, t.Time) >= datePick &&
                                                EntityFunctions.AddDays(t.TimeStart, t.Time) <= EntityFunctions.AddDays(datePick, time))
                                    .Select(e => e.EmployeeId);

            var employees = Context.Employees.Where(e => !emps.Contains(e.Id)).ToList();
            return employees;
        }

        public IEnumerable<Employee> GetEmployeesWithPaging(int page, int size)
        {
            return Context.Employees.OrderBy(x => x.Name).Skip(size * (page - 1)).Take(size).ToList();
        }

        public IEnumerable<Employee> SearchEmployees(string employeeSearch)
        {
            return Context.Employees.Where(x => x.Name.Contains(employeeSearch.ToLower())).ToList();
        }

        public IEnumerable<Employee> SearchEmployeesWithPaging(string employeeSearch, int page, int size)
        {
            return Context.Employees.Where(x => x.Name.Contains(employeeSearch.ToLower())).OrderBy(x => x.Name).Skip(size * (page - 1)).Take(size).ToList();
        }
    }
}
