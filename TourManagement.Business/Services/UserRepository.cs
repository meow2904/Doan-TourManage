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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public User GetUserLogin(string email, string password)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email.Trim() && x.Password == password);
        }

        public IEnumerable<User> GetUsersWithPaging(int page, int size)
        {
            return Context.Users.OrderBy(x => x.Id).Skip(size * (page - 1)).Take(size).ToList();
        }

        public User Search(string email)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email.Trim().ToString());
        }
    }
}
