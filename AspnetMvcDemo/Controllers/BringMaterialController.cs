using AspnetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Controllers
{
    public class BringMaterialController : Controller
    {
        QCEntities db = new QCEntities();
        // GET: BringMaterial
        public ActionResult Index()
        {
            BringMaterials model = new BringMaterials();
            model.locations = db.Locations.ToList();
            model.users = db.Users.Where(u => u.Location_Id != null).ToList();
            model.factories = db.Factory11.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult getFact (BringMaterials model)
        {
           
            model.locations = db.Locations.ToList();
            model.users = db.Users.Where(u => u.Location_Id != null).ToList();
            model.factories = db.Factory11.Where(f => f.Location_Id == model.BringMaterialVisit.location_id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(BringMaterials model)
        {
            model.factories = db.Factory11.ToList();
            model.locations = db.Locations.ToList();
            model.users = db.Users.Where(u => u.Location_Id != null).ToList();
            return View(model);
        }

       
    }
}