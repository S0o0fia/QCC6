using AspnetMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspnetMvcDemo.Models
{ 
    public class factorymodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<double> ProductionCapacity { get; set; }
        public Nullable<double> DailyProductionRate { get; set; }
        public Nullable<int> NumberofMixers { get; set; }
        public Nullable<int> NumberofTrucks { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string MailBox { get; set; }
        public string Email { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerExperience { get; set; }
        public string EngineerName { get; set; }
        public string EngineerPhoneNumber { get; set; }
        public string EngineerEmail { get; set; }
        public string EngineerExperience { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string Location { get; set; }
        public Nullable<int> Location_Id { get; set; }
        public Nullable<bool> State { get; set; }

    }
    public class FactoryTemp
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class CreateFactory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Nullable<double> ProductionCapacity { get; set; }
        public Nullable<double> DailyProductionRate { get; set; }
        public Nullable<int> NumberofMixers { get; set; }
        public Nullable<int> NumberofTrucks { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string MailBox { get; set; }
        public string Email { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhoneNumber { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string ManagerEmail { get; set; }
        public Nullable<double> ManagerExperience { get; set; }
        public string EngineerName { get; set; }
        public string EngineerPhoneNumber { get; set; }
        public string EngineerEmail { get; set; }
        public Nullable<double> EngineerExperience { get; set; }
    }

    public class MixingDesignDetails
    {
        public string Name { set; get; }
        public string Area { set; get; }
        public string Owner { set; get; }
        public string ConcreteRank { set; get; }
        public string Comments { set; get; }
        public string Status { set; get; }
    }

    public class Fact
    {
        public int FactoryId { get; set; }
        public string FactoryName { get; set; }
    }

    public class Mon
    {
        public int MonitorId { get; set; }
        public string MonitorName { get; set; }
    }
    public class TestsPathMixing : TestsMixingDesign
    {
        public HttpPostedFileBase[] files { get; set; }

    }
    public class ConcreteSamplesWithPath : ConcreteSample1
    {
        public int FactoryId { get; set; }

        public HttpPostedFileBase[] files { get; set; }

        public HttpPostedFileBase[] CleanDocfiles { get; set; }
        public HttpPostedFileBase[] DustDocfiles { get; set; }
        public HttpPostedFileBase[] SummerDocfiles { get; set; }
        public HttpPostedFileBase[] LabDocfiles { get; set; }
        public HttpPostedFileBase[] TruckDocfiles { get; set; }
        public HttpPostedFileBase[] SafteyDocfiles { get; set; }
        public string Reason1 { get; set; }
        public string Reason2 { get; set; }


    }

    public class ConcreateTests
    {
        public List<TestsPathMixing> Tests { set; get; }
        public int MixingId { set; get; }
        public string IsConcrete { set; get; }

    }
    public class AddMixDesign : ConcreteMixingDesign
    {
        public List<Fact> Factories { get; set; }
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }

    }

    public class Path
    {
        public string Filename { get; set; }
        public string PathName { get; set; }
    }
    public class ApproveData
    {
        public int id { get; set; }
        public string Status { get; set; }

    }
    public class ApproveMixingDesign : ConcreteMixingDesign
    {
        public string FactoryName { get; set; }
        public string OwnerName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsApprove { get; set; }
        public List<Path> Paths { get; set; }
        public List<Path> ConcreatePaths { get; set; }
        public bool IsConcrete { get; set; }

    }

    public class AdminVisit
    {
        [Key]
        public long Id { get; set; }
        public long MonitorId { get; set; }
        public long FactoryId { get; set; }
        public string Monitor { get; set; }
        public DateTime? VisitDate { get; set; }
        public string FactoryName { get; set; }
        public string FactoryLocation { get; set; }
        public List<Mon> RemainingMonitors { get; set; }
    }

    public class EditVisit
    {
        public List<string> Monitor { get; set; }
        public List<string> FactoryName { get; set; }
        public DateTime? VisitDate { get; set; }
    }

    public class VisitDetailsModel
    {
        public List<Factory11> TodayVisits { get; set; }
        public List<ConcreteSample1> ReceiveSamples { get; set; }
        public List<Factory11> BrokenSamples { get; set; }

        public List<AdminVisit> TotalVisits { get; set; }
    }

    public class BringMaterials
    {
        public List<User> users { get; set; }

        public List<Factory11> factories { get; set; }

        public List<Location> locations { get; set; }

        public BringMaterialVisit BringMaterialVisit { get; set; }

        public List<MaterialforBringVisit> materials { get; set; }

    }

    public class LaboratoryModel
    {
        public List<ConcreteSample1> TodaySamples { get; set; }
        public List<ConcreteSample1> SevenDaysSamples { get; set; }
        public List<ConcreteSample1> TestedSamples { get; set; }
    }

    public class SampleReport
    {
        [Key]
        public long? SampleNumber { get; set; }
        public DateTime? ReportedDate { get; set; }
        public string FactoryName { get; set; }
        public string MonitorName { get; set; }
        public string CustomerName { get; set; }
    }
    public class Alerts
    {
        public int AlertID { get; set; }
        public string FactoryName { get; set; }

        public string Status { get; set; }


    }
    public class PressureResistanceTestforFactorySamplePhoto : PressureResistanceTestforFactorySample
    {
        public string Con { get; set; }

        public string Photo1 { get; set; }
        public string Photo2 { get; set; }

    }
    public class InfractionDetails : Infraction
    {
        public string FactoryName { get; set; }
        public string Location { get; set; }

    }
    public class InfractionDetail
    {
        public string FactoryName { get; set; }
        public string Location { get; set; }
        public User User { set; get; }
        public Infraction infraction { get; set; }
        public List<ConcreteSample1> infractions { get; set; }
        public int InfractionCount { get; set; }
        public int VisitsCount { get; set; }

    }
    public class REviewMaterials : ReviewMaterial
    {
        public List<Location> Locations { set; get; }
        public List<Factory11> factories { set; get; }

    }
    public class InfractionAlert
    {

        public int AlertId { set; get; }
        public string Status { set; get; }
        public User User { set; get; }
        public List<Infraction> Infractions { set; get; }

    }
    public class CubeSamplesWithPath : SampleCubePath
    {
        public HttpPostedFileBase[] Cube1P1Path { get; set; }
        public HttpPostedFileBase[] Cube1P2Path { get; set; }
        public HttpPostedFileBase[] Cube2P1Path { get; set; }
        public HttpPostedFileBase[] Cube2P2Path { get; set; }
        public HttpPostedFileBase[] Cube3P1Path { get; set; }
        public HttpPostedFileBase[] Cube3P2Path { get; set; }
        public int ConcreteRank { get; set; }
    }

    public class threeCubeOfSevenDaysSample
    {
        public sevenDaysResult SamplePart1 { get; set; }
        public sevenDaysResult SamplePart2 { get; set; }
        public sevenDaysResult SamplePart3 { get; set; }

    }

    public class threeCubeOfMonthlySample
    {
        public monthlyResult SamplePart1 { get; set; }
        public monthlyResult SamplePart2 { get; set; }
        public monthlyResult SamplePart3 { get; set; }

    }

    public class CircularsPath : Circular
    {
        public string factId { get; set; }
        public HttpPostedFileBase[] circularFileName { get; set; }
        public IEnumerable<SelectListItem> facts { get; set; }

    }

    public class allCircularsDisplay
    {
        public string fileName { get; set; }
        public string factoryName { get; set; }
        public string location { get; set; }
        public DateTime? date { get; set; }
        public bool? downloadState { get; set; }

    }

    public class datePickerMonthlyReport
    {
        public List<MonthlyReportViewModel> monthlyReportViews { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }

    class ChooseFactroy
    {
        public List<Location> Locations { get; set; }
    }


    public class tempretureClass
    {
        public List<temprutureCheck> temprutureChecks { get; set; }
        public temprutureCheck tempruture { get; set; }
    }

    public class MachineClass
    {
        public List<MachineCheck> machineChecks { get; set; }
        public MachineCheck machine { get; set; }
    }
}