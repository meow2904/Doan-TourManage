using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class UsersManagementController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private IUserRepository _userRepository;

        public UsersManagementController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: Admin/UsersManagement
        public ActionResult Index(int page)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                var totalUsers = _userRepository.GetAll().Count();
                if (page <= 0)
                {
                    page = 1;
                }
                var totalPage = (int)Math.Ceiling(totalUsers / (double)8);
                ViewBag.TotalPage = totalPage;
                ViewBag.CurrentPage = page;

                var users = _userRepository.GetUsersWithPaging(page, 8);
                return View(users);
            }
        }


        public void ResetPassword(int userId)
        {
            var user = _userRepository.GetById(userId);
            user.Password = "000000";
            _userRepository.Update(user);
        }

        public ActionResult Search(string employeeSearch)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                var employees = _userRepository.Search(employeeSearch);
                return View(employees);
            }
        }
    }
}
