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
    public class RoomsManagementController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private readonly IRoomRepository _roomRepository;
        private const string _ImagesPath = "~/Content/images/hotels/room";

        public RoomsManagementController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: Admin/RoomsManagement
        public ActionResult Index(int hotelId)
        {
            var rooms = _roomRepository.GetRoomByHotelId(hotelId);
            return View(rooms);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Room room, HttpPostedFileBase filesInput)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filesInput != null && filesInput.ContentLength > 0)
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

                var resultAdd = _roomRepository.Add(room);

                if (resultAdd)
                {
                    return Content($"<script language='javascript' type='text/javascript'> alert('Thêm thành công'); window.location.href='https://localhost:44316/Admin/HotelsManagement/Edit/'+{room.HotelId} </script>");
                }
            }
            return Content("<script language='javascript' type='text/javascript'> alert('Thêm thất bại '); window.location.href='https://localhost:44316/Admin/HotelsManagement' </script>");
        }

        // GET: Admin/RoomsManagement/Edit/5
        public ActionResult GetRoom(int roomId)
        {
            var room = _roomRepository.GetById(roomId);
            return PartialView(room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Room room, HttpPostedFileBase filesInput)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (filesInput != null && filesInput.ContentLength > 0)
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

                _roomRepository.Update(room);

                return Content($"<script language='javascript' type='text/javascript'> alert('Cập nhật thành công'); window.location.href='https://localhost:44316/Admin/RoomsManagement/?hotelId='+{room.HotelId} </script>");
            }

            return Content($"<script language='javascript' type='text/javascript'> alert('Cập nhật thất bại '); window.location.href='https://localhost:44316/Admin/RoomsManagement/?hotelId='+{room.HotelId} </script>");
        }

        // POST: Admin/RoomsManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int hoId)
        {
            _roomRepository.DeleteByID(id);
            return RedirectToAction("Index", "RoomsManagement", new { area = "Admin", hotelId = hoId });
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
