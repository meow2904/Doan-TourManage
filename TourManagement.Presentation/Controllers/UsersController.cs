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

namespace TourManagement.Presentation.Controllers
{
    public class UsersController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Bind(Include = "Id,Name,Phone,Email,Address,Password,Role")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = 2;
                var result = _userRepository.Add(user);
                if(result)
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('Bạn đã đăng ký thành công!'); window.location.href='https://localhost:44316/'</script>");
                }
            }

            return Redirect("/");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _userRepository.GetById((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = 2;
                _userRepository.Update(user);
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/");
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var result = _userRepository.GetUserLogin(user.Email, user.Password);

            if (result != null && result.Role == 2)
            {
                Session["username"] = result;
                return RedirectToAction("Index", "Home");

                
            }
            else if (result != null && result.Role == 1)
            {
                Session["username"] = result;
                return RedirectToAction("Index", "ToursManagement", new { area = "Admin" });
            }
            else
            {
                ViewBag.MessageError = "Tài khoản hoặc mật khẩu không chính xác !";
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
