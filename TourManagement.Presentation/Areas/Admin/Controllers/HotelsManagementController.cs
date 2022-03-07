using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Business.IServices;
using TourManagement.Models.DBContext;

namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class HotelsManagementController : Controller
    {
        //private TourManagementContext db = new TourManagementContext();
        private readonly IHotelRepository _hotelRepository;
        private const string _ImagesPath = "~/Content/images/hotels";

        public HotelsManagementController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public ActionResult Index()
        {
            var hotels = _hotelRepository.GetAll();
            return View(hotels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Hotel hotel, HttpPostedFileBase filesInput)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filesInput != null)
                {
                    try
                    {
                        fileName = Path.GetFileName(filesInput.FileName);
                        string path = Path.Combine(Server.MapPath(_ImagesPath), fileName);
                        filesInput.SaveAs(path);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                hotel.Image = fileName;
                var resultAdd = _hotelRepository.Add(hotel);
                if (resultAdd)
                {
                    return Content($"<script language='javascript' type='text/javascript'> alert('Thêm thành công'); window.location.href='https://localhost:44316/Admin/HotelsManagement' </script>");
                }

            }

            return View(hotel);
        }

        // GET: Admin/HotelsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var hotel = _hotelRepository.GetById((int)id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Hotel hotel, HttpPostedFileBase filesInput)
        {
            if (ModelState.IsValid)
            {
                if (filesInput != null)
                {
                    try
                    {

                        string fileName = "";
                        fileName = Path.GetFileName(filesInput.FileName);
                        string path = Path.Combine(Server.MapPath(_ImagesPath), fileName);
                        filesInput.SaveAs(path);

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                _hotelRepository.Update(hotel);
                return Content($"<script language='javascript' type='text/javascript'> alert('Cập nhật thành công'); window.location.href='https://localhost:44316/Admin/HotelsManagement' </script>");
            }
            return View(hotel);
        }

        // POST: Admin/HotelsManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _hotelRepository.DeleteByID(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
