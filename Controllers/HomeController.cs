using QuanLyCongViec.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCongViec.Controllers
{
    public class HomeController : Controller
    {
        public readonly DataContext _context;
        public HomeController()
        {
            _context = new DataContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadTable()
        {
            var data = _context.CongViecs.Include(x => x.Cons ).Where(x => x.ChaId == null || x.ChaId == 0).AsQueryable();
            
            return View(data.ToList());
        }
        [HttpGet]
        public ActionResult AddOrUpdate(int id = 0, int chaId = 0)
        {
            var nguoiDungs = new List<NguoiDung> {
                new NguoiDung(1,"Trần Phước Dư"),
                new NguoiDung(2,"Trần Nguyễn Đăng Khoa"),
                new NguoiDung(3,"Nguyễn Minh Luân"),
                new NguoiDung(4,"Văng Quang Trung"),
                new NguoiDung(5,"Thanh Hoài"),
                new NguoiDung(6,"Tuấn Kiệt")
            };
            if (id == 0)
            {
                ViewBag.NguoiDuocGiao = new SelectList(nguoiDungs, "Id", "Name",1);
                ViewBag.NguoiThucHien = new SelectList(nguoiDungs, "Id", "Name",1);
                var entity = new CongViec();
                if (chaId != 0)
                {
                    entity.ChaId = chaId;
                    ViewBag.TenCha = _context.CongViecs.Where(x => x.CongViecId == chaId).FirstOrDefault()?.Ten;
                }
                return View(entity);
            }
            else
            {
                var entity = _context.CongViecs.Where(x => x.CongViecId == id).FirstOrDefault();
                if (entity == default(CongViec))
                    throw new Exception("Không tìm thấy dữ liệu");
                ViewBag.NguoiDuocGiao = new SelectList(nguoiDungs,"Id","Name", entity.NguoiDuocGiao);
                ViewBag.NguoiThucHien = new SelectList(nguoiDungs,"Id","Name", entity.NguoiThucHien);
                ViewBag.TenCha = _context.CongViecs.Where(x => x.CongViecId == chaId).FirstOrDefault()?.Ten;
                return View(entity);
            }
        }

        [HttpPost]
        public ActionResult AddOrUpdate(CongViec model)
        {
            try
            {
                if (model.CongViecId == 0)
                {
                    _context.CongViecs.Add(model);
                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Thêm thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var entity = _context.CongViecs.Where(x => x.CongViecId == model.CongViecId).FirstOrDefault();
                    if (entity == default(CongViec))
                        throw new Exception("Không tìm thấy dữ liệu.");
                    entity.Ten = model.Ten;
                    entity.MoTa = model.MoTa;
                    entity.NgayBatDau = model.NgayBatDau;
                    entity.NgayKetThuc = model.NgayKetThuc;
                    entity.NguoiDuocGiao = model.NguoiDuocGiao;
                    entity.NguoiThucHien = model.NguoiThucHien;
                    if (model.ChaId != 0 && model.ChaId != null)
                        entity.ChaId = model.ChaId;
                    _context.SaveChanges();
                    return Json(new GenericMessageVM()
                    {
                        Status = true,
                        Message = $"Cập nhật thành công!",
                        MessageType = GenericMessage.success,
                        Data = model
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new GenericMessageVM()
                {
                    Status = false,
                    Message = $"{ex.Message}",
                    MessageType = GenericMessage.error
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = _context.CongViecs.Where(x => x.CongViecId == id).FirstOrDefault();
                _context.CongViecs.Remove(data);
                _context.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CongViecCaNhan()
        {
            return View();
        }
        public ActionResult LoadTableCongViecCaNhan()
        {
            var data = _context.CongViecs.Include(x => x.Cons).Where(x => x.NguoiThucHien == 6).AsQueryable();
            return View(data.ToList());
        }
        public ActionResult CongViecTrongNgay()
        {
            return View();
        }
        public ActionResult LoadTableCongViecTrongNgay()
        {
            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var data = _context.CongViecs.Include(x => x.Cons)
                                .Where(x => x.NguoiThucHien == 6
                                    && (x.NgayBatDau <= now && now <= x.NgayKetThuc)
                                ).AsQueryable();
            
            return View(data.ToList());
        }
    }
}