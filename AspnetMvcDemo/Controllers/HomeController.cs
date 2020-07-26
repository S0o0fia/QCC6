 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using Resources;

namespace AspnetMvcDemo.Controllers
{       
    public class HomeController : Controller
    {
        QCEntities db = new QCEntities();

        public ActionResult MakeChoice()
        {
            return View();
        }


        public ActionResult Home(int id)
        {
            if (id == 1)
                Session["Choice"] = "Concrete";
            if(id == 2)
                Session["Choice"] = "Block";

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();

            VisitService visitService = new VisitService();
            model.TodayVisits = visitService.getTodayVisits(userId);

            model.TotalVisits = visitService.getTotalVisits();

            return View(model);
        }

        public ActionResult ReceiveSample (int id = 1)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();
            if(@Session["JobTitle"].ToString() == "Admin")
            {
                model.ReceiveSamples = visitService.ReceiveSample();
            }
            else
            {
                model.ReceiveSamples = visitService.ReceiveSample(userId);
            }
            return View (model);
        }

        [HttpPost]
        public ActionResult ReceiveSample(VisitDetailsModel fact , string id)
        {
            var yesterday = DateTime.Now;
            var factname = id;
            var report = db.ConcreteSample1.Where(s => s.FactoryName == factname && DbFunctions.DiffDays(s.ReportDate , yesterday)== 1 ).FirstOrDefault();
            report.IsReceived = true;
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return RedirectToAction("ReceiveSample");
            }

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();

            model.ReceiveSamples = visitService.ReceiveSample(userId);

            return RedirectToAction("ReceiveSample");
        }

        public ActionResult BrokenSample (int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            var userId = Convert.ToInt32(Session["UserId"].ToString());

            VisitDetailsModel model = new VisitDetailsModel();
            VisitService visitService = new VisitService();

            model.BrokenSamples = visitService.BrokenSample(userId);
            return View(model);
        }

        [HttpGet]
        public ActionResult VisitDetails(int Id)
        {
            var visitDetails = from f in db.Factory11
                              join v in db.VisitDetails on f.Id equals v.FactoryId
                              join u in db.Users on v.MonitorId equals u.Id
                              where v.Id== Id
                              orderby v.VisitDate
                              select new AdminVisit { MonitorId=u.Id, FactoryId=f.Id, Monitor =u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName=f.Name, FactoryLocation=f.Location };

            var monitors = db.Users.Where(u => u.JobTitle == "Monitor").Select(x => new Mon { MonitorId = x.Id, MonitorName = x.FullName }).ToList().ToList();

            var visit = visitDetails.FirstOrDefault();

            AdminVisit result = new AdminVisit
            {
                Id=Id,
                MonitorId = visit.MonitorId,
                FactoryId=visit.FactoryId,
                Monitor = visit.Monitor,
                FactoryName=visit.FactoryName,
                FactoryLocation=visit.FactoryLocation,
                VisitDate=visit.VisitDate,
                RemainingMonitors=monitors
            };

            return PartialView(result);
        }

        [HttpPost]
        public ActionResult UpdateVisit(VisitDetail visit)
        {
            ObjectParameter statusCode = new ObjectParameter("StatusCode", typeof(int));
            ObjectParameter statusMessage = new ObjectParameter("StatusMessage", typeof(string));

            var result = db.AddUpdateVisit(visit.Id, visit.FactoryId, visit.MonitorId, visit.VisitDate, statusCode, statusMessage);

            return RedirectToAction("Home", "Home", new
            {
                id = 1
            });
        }
    }
}