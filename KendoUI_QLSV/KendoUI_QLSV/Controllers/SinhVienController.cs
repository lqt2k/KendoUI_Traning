using KendoUI_QLSV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using KendoUI_QLSV.Models.subModels;
using Kendo.Mvc.Extensions;

namespace KendoUI_QLSV.Controllers
{
    public class SinhVienController : Controller
    {
        public static QLSINHVIENEntities db = new QLSINHVIENEntities();
        // GET: SinhVien
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getData([DataSourceRequest] DataSourceRequest request)
        {
            _SinhVien sinhvien = new _SinhVien();
            var sv = db.SinhVien.ToList();
            List<_SinhVien> lstSV = new List<_SinhVien>();
            lstSV.Clear();
            sv.ForEach(i =>
            {
                sinhvien = new _SinhVien();
                sinhvien.MaSinhVien = i.MaSinhVien;
                sinhvien.HoTen = i.HoTen;
                //sinhvien.NgaySinh = i.NgaySinh;
                sinhvien.MaLop = i.MaLop;
                lstSV.Add(sinhvien);
            });
            return this.Json(lstSV.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //Insert SinhVien
        public JsonResult Create_SinhVien([DataSourceRequest] DataSourceRequest request, SinhVien sinhvien)
        {
            if(sinhvien != null && ModelState.IsValid)
            {
                SinhVien sv = new SinhVien();              
                sv.HoTen = sinhvien.HoTen;
                //sv.NgaySinh = sinhvien.NgaySinh;
                sv.MaLop = sinhvien.MaLop;
                db.SaveChanges();
                sinhvien.MaSinhVien = sv.MaSinhVien;
            }    
            return Json(new[] { sinhvien }.ToDataSourceResult(request, ModelState));
        }

        //Update SinhVien
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update_SinhVien([DataSourceRequest] DataSourceRequest request, SinhVien sinhvien)
        {
            if (sinhvien != null && ModelState.IsValid)
            {
                SinhVien sv = db.SinhVien.Single(c => c.MaSinhVien == sinhvien.MaSinhVien);
                sv.HoTen = sinhvien.HoTen;
                sv.MaLop = sinhvien.MaLop;
            }
            db.SaveChanges();
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }

        //Delete SinhVien
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete_SinhVien([DataSourceRequest] DataSourceRequest request, SinhVien sinhvien)
        {
            if (sinhvien != null && ModelState.IsValid)
            {
                SinhVien sv = db.SinhVien.Single(c => c.MaSinhVien == sinhvien.MaSinhVien);
                db.SinhVien.Remove(sv);
            }
            db.SaveChanges();
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}