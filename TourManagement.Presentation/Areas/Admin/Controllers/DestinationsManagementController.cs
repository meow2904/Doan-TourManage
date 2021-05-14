using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TourManagement.Models.DBContext;
using TourManagement.Business.IServices;
namespace TourManagement.Presentation.Areas.Admin.Controllers
{
    public class DestinationsManagementController : Controller
    {
        private TourManagementContext db = new TourManagementContext();
        private readonly IDestinatioRepository _destinatioRepository;
        public DestinationsManagementController(IDestinatioRepository destinatioRepository)
        {
            _destinatioRepository = destinatioRepository;
        }

        [HttpPost]
        public ActionResult Create(Destination destination)
        {
            if (ModelState.IsValid)
            {
                _destinatioRepository.Add(destination);
                return Content("<script language='javascript' type='text/javascript'>alert('Thêm thành công!'); window.location.href='https://localhost:44316/Admin/ToursManagement/Create'</script>");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Thêm thất bại '); window.location.href='https://localhost:44316/Admin/ToursManagement/Create'</script>");
            }    
        }

        // GET: Admin/DestinationsManagement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Admin/DestinationsManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: Admin/DestinationsManagement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return HttpNotFound();
            }
            return View(destination);
        }

        // POST: Admin/DestinationsManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            db.Destinations.Remove(destination);
            db.SaveChanges();
            return RedirectToAction("Index");
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
