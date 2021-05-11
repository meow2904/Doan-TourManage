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
    public class EmployeesManagementController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITourRepository _tourRepository;
        public EmployeesManagementController(IEmployeeRepository employeeRepository,
            ITourRepository tourRepository)
        {
            _employeeRepository = employeeRepository;
            _tourRepository = tourRepository;
        }

        // GET: Admin/EmployeesManagement
        public ActionResult Index(int page)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                var totalEmployees = _employeeRepository.GetAll().Count();
                if (page <= 0)
                {
                    page = 1;
                }
                var totalPage = (int)Math.Ceiling(totalEmployees / (double)8);
                ViewBag.TotalPage = totalPage;
                ViewBag.CurrentPage = page;

                var employees = _employeeRepository.GetEmployeesWithPaging(page, 8);
                return View(employees);
            }

        }

        public ActionResult Search(string employeeSearch, int page)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                var totalEmployees = _employeeRepository.SearchEmployees(employeeSearch).Count();
                if (page <= 0)
                {
                    page = 1;
                }
                var totalPage = (int)Math.Ceiling(totalEmployees / (double)8);
                ViewBag.TotalPage = totalPage;
                ViewBag.CurrentPage = page;

                var employees = _employeeRepository.SearchEmployeesWithPaging(employeeSearch, page, 6);
                return PartialView(employees);
            }
        }


        public ActionResult Details(int? id)
        {

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                var employee = _employeeRepository.GetById((int)id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }

        }


        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    employee.StatusWorking = 1;
                    var result = _employeeRepository.Add(employee);
                    if (result)
                    {
                        return Content("<script language='javascript' type='text/javascript'>alert('Bạn đã đăng ký thành công!'); window.location.href='https://localhost:44316/'</script>");
                    }
                    return RedirectToAction("Index", "EmployeesManagement", new { area = "Admin" });
                }

                return View(employee);
            }
        }


        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = _employeeRepository.GetById((int)id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,BirthDate,Address,Phone,StatusWorking")] Employee employee)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Users", new { area = "" });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _employeeRepository.Update(employee);
                    return RedirectToAction("Index", new { page = 1 });
                }
                return View(employee);
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            _employeeRepository.Delete(employee);
            return RedirectToAction("Index", new { page = 1 });
        }

    }
}
