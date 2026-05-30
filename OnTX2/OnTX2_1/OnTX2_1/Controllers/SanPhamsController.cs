using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnTX2_1.Models;

namespace OnTX2_1.Controllers
{
    public class SanPhamsController : Controller
    {
        private TraiCayContext db = new TraiCayContext();
        public ActionResult GetHangSX()
        {
            return View(db.HangSanXuats.ToList());
        }

        // GET: SanPhams
        public ActionResult Index(string searchString)
        {
            var sanPhams = db.SanPhams.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int donGia))
                {
                    sanPhams = sanPhams.Where(sp => sp.DonGia >= donGia);
                }
                else
                {
                    ModelState.AddModelError("searchString", "Đơn giá tìm phải là số nguyên!");
                }
            }
            
            
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,HinhAnh,MaHang")] SanPham sanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (!db.SanPhams.Any(sp=>sp.MaSP == sanPham.MaSP))
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(file.FileName);
                        string path = Server.MapPath("~/images/" + fileName);
                        file.SaveAs(path);
                        sanPham.HinhAnh = fileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("MaSP", "Trùng mã sản phẩm");
                }
            }

            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,HinhAnh,MaHang")] SanPham sanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/images/" + fileName);
                    file.SaveAs(path);
                    sanPham.HinhAnh = fileName;
                }
                else
                {
                    sanPham.HinhAnh = db.SanPhams.AsNoTracking().SingleOrDefault(sp => sp.MaSP == sanPham.MaSP).HinhAnh;
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHang = new SelectList(db.HangSanXuats, "MaHang", "TenHang", sanPham.MaHang);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            string path = Server.MapPath("~/images/" + sanPham.HinhAnh);
            System.IO.File.Delete(path);
            db.SanPhams.Remove(sanPham);
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
