using AspnetMvcDemo.Models;
using AspnetMvcDemo.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspnetMvcDemo.Controllers
{
    public class TestingResultsController : Controller
    {
        concreteSampleSer ConSer = new concreteSampleSer();
        public ActionResult Index(int id)
        {
            Session["Choice"] = id == 1 ? "Concrete" : "Block";


            var Concrete = ConSer.ViewIndex();
            return View(Concrete);
        }

        public ActionResult ConcreteTestingResult(int id)
        {
            var qu = ConSer.TestForCompany(id);
            return View(qu);
        }
        [HttpPost]
        public ActionResult ConcreteTestingResult(List<PressureResistanceTestforFactorySamplePhoto> PressList)
        {
            ConSer.SaveAll(PressList);
            return RedirectToAction("Index", new
            {
                id = 1
            });
        }

        public ActionResult SevenDaysResult(int id)
        {
            //var qu = ConSer.TestForCompany(id);
            //return View(qu);
            return View();
        }
        [HttpPost]
        public ActionResult SevenDaysResult(threeCubeOfSevenDaysSample cubeOfOneSample)
        {
            QCEntities qc = new QCEntities();
            
            cubeOfOneSample.SamplePart1.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart1.volume = 5302;
            cubeOfOneSample.SamplePart1.areaLoaded = 17674;
            cubeOfOneSample.SamplePart1.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart1.age = 7;

            cubeOfOneSample.SamplePart2.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart2.volume = 5302;
            cubeOfOneSample.SamplePart2.areaLoaded = 17674;
            cubeOfOneSample.SamplePart2.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart2.age = 7;

            cubeOfOneSample.SamplePart3.sampleNumber = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart3.volume = 5302;
            cubeOfOneSample.SamplePart3.areaLoaded = 17674;
            cubeOfOneSample.SamplePart3.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart3.age = 7;

            cubeOfOneSample.SamplePart1.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;
            cubeOfOneSample.SamplePart2.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;
            cubeOfOneSample.SamplePart3.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;

            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart1);
            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart2);
            qc.sevenDaysResults.Add(cubeOfOneSample.SamplePart3);
            qc.SaveChanges();

            return RedirectToAction("SevenDaysTest", "Laboratory", new
            {
                id = 1
            });
        }

        public ActionResult MonthlyResult(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult MonthlyResult(threeCubeOfMonthlySample cubeOfOneSample)
        {
            QCEntities qc = new QCEntities();
            int SampleNumberFromUrl = int.Parse(RouteData.Values["id"].ToString());
            cubeOfOneSample.SamplePart1.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart1.volume = 5302;
            cubeOfOneSample.SamplePart1.areaLoaded = 17674;
            cubeOfOneSample.SamplePart1.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart1.age = 28;

            cubeOfOneSample.SamplePart2.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart2.volume = 5302;
            cubeOfOneSample.SamplePart2.areaLoaded = 17674;
            cubeOfOneSample.SamplePart2.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart2.age = 28;

            cubeOfOneSample.SamplePart3.sampleNumber = SampleNumberFromUrl;
            cubeOfOneSample.SamplePart3.volume = 5302;
            cubeOfOneSample.SamplePart3.areaLoaded = 17674;
            cubeOfOneSample.SamplePart3.testDate = DateTime.Now;
            cubeOfOneSample.SamplePart3.age = 28;

            cubeOfOneSample.SamplePart1.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;
            cubeOfOneSample.SamplePart2.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;
            cubeOfOneSample.SamplePart3.averageCompressiveStrength = (cubeOfOneSample.SamplePart1.CompressiveStrength + cubeOfOneSample.SamplePart2.CompressiveStrength + cubeOfOneSample.SamplePart3.CompressiveStrength) / 3;

            qc.monthlyResults.Add(cubeOfOneSample.SamplePart1);
            qc.monthlyResults.Add(cubeOfOneSample.SamplePart2);
            qc.monthlyResults.Add(cubeOfOneSample.SamplePart3);

            var cRank = qc.ConcreteSample1.Where(s => s.SampleNumber == SampleNumberFromUrl).Select(c => c.ConcreteRank).FirstOrDefault();
            int concRanck = int.Parse((cRank.Split('-'))[1]);

            var qInfrac = qc.Infractions.Where(inf => inf.SampleNo == SampleNumberFromUrl).FirstOrDefault();

            var fact = (from conSamp in qc.ConcreteSample1
                        join fac in qc.Factory11
                        on conSamp.FactoryName equals fac.Name
                        where conSamp.SampleNumber == SampleNumberFromUrl
                        select new
                        {
                            FactoryId = fac.Id,
                            createdDate = conSamp.CreatedDate
                        }).FirstOrDefault();

            switch (concRanck)
            {
                case 35:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 34.5)
                        {
                            
                             if(qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                             else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                            break;
                    }
                case 30:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 29.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                case 20:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 19.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                case 15:
                    {
                        if (cubeOfOneSample.SamplePart1.averageCompressiveStrength < 14.5)
                        {

                            if (qInfrac == null)
                            {
                                Infraction infraction = new Infraction();
                                {
                                    infraction.FactoryId = fact.FactoryId;
                                    infraction.Temperature = false;
                                    infraction.Landing = false;
                                    infraction.C8Day = true;
                                    infraction.VisitDate = fact.createdDate;
                                    infraction.SampleNo = SampleNumberFromUrl;
                                    infraction.AdminApproved = false;
                                    infraction.MonitorApproved = false;
                                    infraction.IsCleanLocation = false;
                                    infraction.NotUsingMixtureofClass = false;
                                    infraction.AbsenceofDevice = false;
                                    infraction.HardwareNotCalibrated = false;
                                    infraction.InsufficientRecords = false;
                                }
                                qc.Infractions.Add(infraction);
                                qc.SaveChanges();
                            }
                            else
                            {
                                qInfrac.C8Day = true;
                                qc.SaveChanges();


                            }
                        }
                        break;
                    }
                default:
                    break;
            }

            qc.SaveChanges();

            return RedirectToAction("MonthTest", "Laboratory", new
            {
                id = 1
            });
        }





        public ActionResult BlockTestingResult()
        {
            return View();
        }
    }
}