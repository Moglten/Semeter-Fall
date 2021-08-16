using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Semester_Fall.Repository;
using Semester_Fall.Models;
using System.Web.Services;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Semester_Fall.Controllers
{
    public class SectionController : Controller
    {
        private readonly UnitOfWork _unitOfwork = new UnitOfWork();
        private readonly MethodsHandler _methodsHandler = new MethodsHandler();

        public ActionResult Index()
        {
            ViewBag.courses = _methodsHandler.GetRightCourses();
            return View();
        }


        public ActionResult SetWithAdjust(int code,int timeId,int Profid, int hallId)
        {
            if (code == -1 || timeId == -1 || Profid == -1 || hallId == -1)
            {
                ViewBag.alert = "You should choose all labels";
                return View("Index");
            }

            var ViewbagContent = _methodsHandler.Adjust(code, timeId, Profid, hallId);

            ViewBag.course = ViewbagContent["Coursename"];
            ViewBag.period = ViewbagContent["Timmingperiod"];
            ViewBag.prof = ViewbagContent["Professorname"];
            ViewBag.hall = ViewbagContent["Hallname"];

            return View();

        }

        public ActionResult ScheduleTable()
        {

            var tableSections = _methodsHandler.CreateTable();
            
            return View(tableSections);
        }

       
        public ActionResult Reset()
        {
            _methodsHandler.ResetTheTable();
            return View("ScheduleTable", new SectionViewModel[5, 4]);
        }

        public ActionResult Download(string url)
        {
            _methodsHandler.DownloadSchdule(url);
            return View("ScheduleTable");
        }

        [WebMethod]
        [AllowAnonymous]
        public ActionResult GetValidTime()
        {
            var times = _methodsHandler.GetValidTimes();

            return Json(new { Times = times }, JsonRequestBehavior.AllowGet);
        }


        [WebMethod]
        [AllowAnonymous]
        [Obsolete]
        public ActionResult GetAppropriateProff(int code)
        {
            var proffs = _methodsHandler.GetValidProf(code);

            return Json(new { professors = proffs }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetHall()
        {
            var Halls = _unitOfwork.GetRepositoryInstance<Halls>().GetAllEntities();

            return Json(new { halls = Halls }, JsonRequestBehavior.AllowGet);
        }

       

    }
}