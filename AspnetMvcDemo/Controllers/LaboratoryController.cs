using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class LaboratoryController : Controller
    {
        QCEntities db = new QCEntities();

        // GET: Laboratory
        public ActionResult Laboratory(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();

            model.TodaySamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.CreatedDate, DateTime.Now) == 1 ).ToList();

            
         
            return View(model);
        }


        [HttpPost]
        public ActionResult Laboratory(string id)
        {
            int number = int.Parse(id);
            var updated = db.ConcreteSample1.Where(s => s.SampleNumber == number).FirstOrDefault();
            updated.labdeliver = true;
            db.SaveChanges();

            LaboratoryModel model = new LaboratoryModel();
            model.TodaySamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.CreatedDate, DateTime.Now) == 1 && s.labdeliver == false).ToList();
            return View(model);

        }
        //Get The 7 Days

        public ActionResult SevenDaysTest(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();

           
            model.SevenDaysSamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.CreatedDate, DateTime.Now) == 7).ToList();

          
            return View(model);

        }


        public ActionResult MonthTest(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";

            LaboratoryModel model = new LaboratoryModel();
            Nullable<int> month = 28;
            model.TestedSamples = db.ConcreteSample1.Where(s => s.IsReceived == true && DbFunctions.DiffDays(s.CreatedDate, DateTime.Now) == month ).ToList();
            return View(model);

        }


    }
}