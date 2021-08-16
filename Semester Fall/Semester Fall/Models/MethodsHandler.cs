using Microsoft.EntityFrameworkCore;
using Semester_Fall.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SelectPdf;

namespace Semester_Fall.Models
{
    public class MethodsHandler
    {

        private readonly UnitOfWork _unitOfwork = new UnitOfWork();

        public SelectList GetRightCourses()
         {
            var courses = _unitOfwork.GetRepositoryInstance<Courses>()
                                     .GetAllEntitiesIQueryable()
                                     .Where(c => c.Contacthoursperweek > c.Consumedperweek)
                                     .ToList();

            return new SelectList(courses, "Code", "CourseName");

        }


        public Dictionary<string,string> Adjust(int code, int timeId, int Profid, int hallId)
        {
            var sec = new Sections
            {
                Code = code,
                Timeid = timeId,
                Profid = Profid,
                Hallid = hallId
            };
            _unitOfwork.GetRepositoryInstance<Sections>().Add(sec);


            var course = _unitOfwork.GetRepositoryInstance<Courses>().GetEntity(code);
            var period = _unitOfwork.GetRepositoryInstance<TimmingSchedule>().GetEntity(timeId);
            var prof = _unitOfwork.GetRepositoryInstance<Professors>().GetEntity(Profid);
            var hall = _unitOfwork.GetRepositoryInstance<Halls>().GetEntity(hallId);

            period.Occupied = true;
            course.Consumedperweek += period.Period;
            prof.Committedto += 1;

            _unitOfwork.Savechanges();

            var dic = new Dictionary<string, string>()
            {
                {"Coursename",course.Coursename},
                {"Timmingperiod", period.Timmingperiod},
                {"Professorname",prof.Professorname},
                {"Hallname",hall.Hallname}
            };

            return dic;
        }

        public SectionViewModel[,] CreateTable()
        {
            var section = new SectionViewModel[5, 4];

            var keyValueDays = new Dictionary<string, int>(){
                 {"Sunday",0}
                ,{"Monday",1}
                ,{"Tuesday",2}
                ,{"Wednesday",3}
                };
            var keyValuePeriod = new Dictionary<string, int>(){
                 {"9:00AM-10:30AM",0}
                ,{"10:30AM-12:00PM",1}
                ,{"12:00PM-1:30PM",2}
                ,{"1:30PM-3:00PM",3}
                ,{"3:00PM-4:30PM",4}
                };

            var CourseColor = new List<string>() { "", "bg-sky", "bg-yellow", "bg-green", "bg-pink", "bg-purple" };



            var sections = _unitOfwork.GetRepositoryInstance<Sections>().GetAllEntities().ToList();

            foreach (var sec in sections)
            {
                var course = _unitOfwork.GetRepositoryInstance<Courses>().GetEntity(sec.Code);
                var period = _unitOfwork.GetRepositoryInstance<TimmingSchedule>().GetEntity(sec.Timeid);
                var prof = _unitOfwork.GetRepositoryInstance<Professors>().GetEntity(sec.Profid);
                var hall = _unitOfwork.GetRepositoryInstance<Halls>().GetEntity(sec.Hallid);

                var time = period.Timmingperiod.Replace(" ", "").Split(',');

                section[keyValuePeriod[time[1]], keyValueDays[time[0]]] = new SectionViewModel
                {
                    ProfName = prof.Professorname,
                    HallName = hall.Hallname,
                    CourseName = course.Coursename,
                    CourseColor = CourseColor[course.Code]
                };
            }

            return section;

        }

        public void ResetTheTable()
        {
            _unitOfwork.getContext().Sections.RemoveRange(_unitOfwork.getContext().Sections);

            var courses = _unitOfwork.GetRepositoryInstance<Courses>().GetAllEntities();
            var times = _unitOfwork.GetRepositoryInstance<TimmingSchedule>().GetAllEntities();
            var profs = _unitOfwork.GetRepositoryInstance<Professors>().GetAllEntities();

            foreach (var course in courses)
            {
                course.Consumedperweek = 0;
            }
            foreach (var time in times)
            {
                time.Occupied = false;
            }
            foreach (var prof in profs)
            {
                prof.Committedto = 0;
            }
            _unitOfwork.Savechanges();


        }

        public IEnumerable<TimmingSchedule> GetValidTimes()
        {
            return _unitOfwork.GetRepositoryInstance<TimmingSchedule>()
                                    .GetAllEntitiesIQueryable()
                                    .Where(t => !(t.Occupied))
                                    .ToList();
        }

        public IEnumerable<Professors> GetValidProf(int code)
        {
            return _unitOfwork.getContext().Professors
                                    .FromSqlRaw("EXECUTE dbo.GetAppropriateProff {0}", code)
                                    .AsEnumerable()
                                    .Where(p => p.Teachingload > p.Committedto)
                                    .ToList();
        }

        public void DownloadSchdule(string url)
        {

            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                "A4", true);

            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                "Portrait", true);

            int webPageWidth = 1024;
            try
            {
                webPageWidth = Convert.ToInt32(webPageWidth);
            }
            catch { }

            int webPageHeight = 1024;
            try
            {
                webPageHeight = Convert.ToInt32(1024);
            }
            catch { }

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save("Sample.pdf");

            // close pdf document
            doc.Close();
        }
    }
}
