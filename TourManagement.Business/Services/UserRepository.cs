﻿using System;
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
            return Context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public IEnumerable<User> GetUsersWithPaging(int page, int size)
        {
            return Context.Users.OrderBy(x => x.Name).Skip(size * (page - 1)).Take(size).ToList();
        }

    }
}
