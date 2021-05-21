using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourManagement.Business.BaseServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Business.IServices
{
    public interface IUserRepository: IGenericRepository<User>
    {
        User GetUserLogin(string email, string password);
        IEnumerable<User> GetUsersWithPaging(int page, int size);
        User Search(string email);
    }
}
