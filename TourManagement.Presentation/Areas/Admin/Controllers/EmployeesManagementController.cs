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
        private TourManagementContext db = new TourManagementContext();
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesManagementController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

                var employees = _employeeRepository.GetEmployeesWithPaging(page, 6);
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

        // GET: Admin/EmployeesManagement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/EmployeesManagement/Create
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

        // POST: Admin/EmployeesManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
                    return RedirectToAction("/");
                }

                return View(employee);
            }
        }

        // GET: Admin/EmployeesManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/EmployeesManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,BirthDate,Address,Phone,StatusWorking")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page= 1});
            }
            return View(employee);
        }

        // GET: Admin/EmployeesManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/EmployeesManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
