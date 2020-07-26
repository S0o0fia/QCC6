using AspnetMvcDemo.Models;
using AspnetMvcDemo.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AspnetMvcDemo.Services
{
    public class VisitService
    {
        QCEntities db = new QCEntities();

        //function for getting visits from database 
        public List<Visitviewmodel> GetVisits()
        {
            var query = (from vd in db.VisitDetails
                         join user in db.Users
                         on vd.MonitorId equals user.Id
                         join fact in db.Factory11
                         on vd.FactoryId equals fact.Id
                         select new Visitviewmodel
                         {
                             factroyname = fact.Name,
                             monitorname = user.FullName,
                             visitDate = vd.VisitDate,
                             state = vd.Status
                         }).ToList();

            return query;
        }

        //Function for getting All visits from database For Reports
        public List<VisitDetailsForReports> getVisitsReport()
        {
            var totalVisits = (from cs in db.ConcreteSample1
                               join f in db.Factory11 on cs.FactoryName equals f.Name
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               join u in db.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(cs.ReportDate) <= DbFunctions.TruncateTime(DateTime.Now)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports 
                               {
                                   visitId = v.Id,
                                   factroyname = f.Name,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   monitorname = u.FullName,
                                   location = f.Location
                               }).Distinct().ToList();
            return (totalVisits);
        }

        //Function For Getting Daily Visits From DataBase For Report
        public List<VisitDetailsForReports> getDailyVisitsReport()
        {
            var totalVisits = (from cs in db.ConcreteSample1
                               join f in db.Factory11 on cs.FactoryName equals f.Name
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               join u in db.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(cs.ReportDate) == DbFunctions.TruncateTime(DateTime.Now)
                               orderby cs.ReportDate
                               select new VisitDetailsForReports
                               {
                                   visitId = v.Id,
                                   factroyname = f.Name,
                                   visitDate = DbFunctions.TruncateTime(cs.ReportDate),
                                   monitorname = u.FullName,
                                   location = f.Location
                               }).Distinct().ToList();
            return (totalVisits);
        }

        //Function For getting Monthly Visits For Report
        public List<MonthlyReportViewModel> monthlyReport ()
        {
            var monthlyReport = (from c in db.ConcreteSample1
                                 join s in db.sevenDaysResults
                                 on c.SampleNumber equals s.sampleNumber
                                 join m in db.monthlyResults
                                 on c.SampleNumber equals m.sampleNumber
                                 select new MonthlyReportViewModel
                                 {
                                     factoryName = c.FactoryName,
                                     conctreteRank = c.ConcreteRank,
                                     visitDate = DbFunctions.TruncateTime(c.ReportDate).ToString() ,
                                     SampleNumber = c.SampleNumber,
                                     ConcreteTemperture = c.ConcreteTemperture,
                                     DownAmount = c.DownAmount,
                                     CementType = c.CementType,
                                     IsCleanUsage = c.IsCleanUsage,
                                     IsBasicUsage = c.IsBasicUsage,
                                     IsColumnUsage = c.IsColumnUsage,
                                     IsRoofUsage = c.IsRoofUsage,
                                     IsOtherUsage = c.IsOtherUsage,
                                     OtherReason = c.OtherReason,
                                     SevenDaysAverageCompressiveStrength = s.averageCompressiveStrength,
                                     MonthlyAverageCompressiveStrength = m.averageCompressiveStrength
                                 }).DistinctBy(c=>c.SampleNumber).ToList();
            return (monthlyReport);
        }


      
       

        //function for create new  visit 
        public Boolean CreateVisits(VisitDetail model)
        {
            db.VisitDetails.Add(new VisitDetail
            {
                FactoryId = model.FactoryId,
                MonitorId = model.MonitorId,
                Status = model.Status,
                VisitDate = model.VisitDate
            });
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool CancleVisit(VisitDetail visitDetail)
        {

            DateTime dateTime = DateTime.Today;
            try
            {
                if ((int)dateTime.DayOfWeek == 4)
                {
                    dateTime = dateTime.AddDays(2);
                }
                else if ((int)dateTime.DayOfWeek == 5)
                {
                    dateTime = dateTime.AddDays(1);
                }
                else if ((int)dateTime.DayOfWeek == 3)
                {
                    dateTime = dateTime.AddDays(3);
                }
                else
                {
                    dateTime = dateTime.AddDays(1);
                }

                var dateOfNextDay = db.VisitDetails.Where(v => v.VisitDate == dateTime).FirstOrDefault();
                var visit = db.VisitDetails.Where(v => v.FactoryId == visitDetail.Id && v.VisitDate == DateTime.Today).FirstOrDefault();


                visit.VisitDate = dateOfNextDay.VisitDate;

                dateOfNextDay.VisitDate = DateTime.Today;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //function for edit the already stored visit
        public Boolean EditVisit(Visitviewmodel model)
        {
            //for getting user id 
            var userId = db.Users.Where(u => u.FullName == model.monitorname).Select(u => u.Id).FirstOrDefault();

            //for getting factory id 
            var factoryid = db.Factory11.Where(f => f.Name == model.factroyname).Select(f => f.Id).FirstOrDefault();

            //getting the recored from db
            VisitDetail vd = db.VisitDetails.Where(v => v.Id == model.visitId).FirstOrDefault();
            //change the old values with new one
            vd.MonitorId = userId;
            vd.FactoryId = factoryid;
            vd.VisitDate = model.visitDate;

            //saving 
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        //Function to Get Today Visits
        public List<Factory11> getTodayVisits(int userId)
        {
            var todayVisits = (from f in db.Factory11
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               where v.MonitorId == userId && DbFunctions.TruncateTime(v.VisitDate) == DbFunctions.TruncateTime(DateTime.Now)
                               select f).ToList();
            return (todayVisits);
        }

        //Function to Get Total Visits
        public List<AdminVisit> getTotalVisits()
        {
            var totalVisits = (from f in db.Factory11
                               join v in db.VisitDetails on f.Id equals v.FactoryId
                               join u in db.Users on v.MonitorId equals u.Id
                               where DbFunctions.TruncateTime(v.VisitDate) >= DbFunctions.TruncateTime(DateTime.Now)
                               orderby v.VisitDate
                               select new AdminVisit { Id = v.Id, Monitor = u.FullName, VisitDate = DbFunctions.TruncateTime(v.VisitDate), FactoryName = f.Name, FactoryLocation = f.Location }
                              ).ToList();
            return (totalVisits);
        }

        //Function to get Receive Concrete Sample
        public List<ConcreteSample1> ReceiveSample(int userId)
        {
          
            var receiveSamples = (from C in db.ConcreteSample1
                                  join f in db.Factory11 on C.FactoryName equals f.Name
                                  join vs in db.VisitDetails on f.Id equals vs.FactoryId
                                  where
                                  DbFunctions.DiffDays(C.ReportDate, DateTime.Now) == 1
                                  && vs.MonitorId == userId && C.IsReceived == false

                                  select C).Distinct().ToList();
            return (receiveSamples);
        }

        public List<ConcreteSample1> ReceiveSample()
        {

            var receiveSamples = (from C in db.ConcreteSample1
                                  join f in db.Factory11 on C.FactoryName equals f.Name
                                  join vs in db.VisitDetails on f.Id equals vs.FactoryId
                                  where
                                  DbFunctions.DiffDays(C.ReportDate, DateTime.Now) == 1
                                  && C.IsReceived == false

                                  select C).Distinct().ToList();
            return (receiveSamples);
        }

        //Function to get Broken Sample
        public List<Factory11> BrokenSample(int userId)
        {
            var brokenSamples = (from f in db.Factory11
                                 join v in db.VisitDetails on f.Id equals v.FactoryId
                                 where v.MonitorId == userId && DbFunctions.DiffDays(v.VisitDate, DateTime.Now) == 28
                                 select f).ToList();
            return (brokenSamples);
        }

        public void updatetheVisitState(string factoryname )
        {
            var date = DateTime.Now.Date.Add(new TimeSpan(0, 0, 0)); ;
            int factid = db.Factory11.Where(f => f.Name == factoryname).Select(f => f.Id).FirstOrDefault();
            var visit = db.VisitDetails.Where(v => v.FactoryId == factid  && v.VisitDate == date).FirstOrDefault();
            visit.Status = true;
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
    }
}