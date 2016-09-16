using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketingReportingSystem.Models;

namespace MarketingReportingSystem.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private MRS.Data.Model.Entities me = new MRS.Data.Model.Entities();

        #region ActionResult

        
        [HttpGet]
        public ActionResult Index()
        {
            var List = me.GetDateString().ToList();
            var flist = TempData["Date"] == null ? me.GetDateString().ToList().FirstOrDefault() : TempData["Date"].ToString();
            var reqType = TempData["reqType"] == null ? "1" : TempData["reqType"].ToString();
            var v1 = me.usp_GetActelRecordsINof(1, flist).ToList();
            var v2 = me.usp_GetActelRecordsINof(2, flist).ToList();
            var v3 = me.usp_GetActelRecordsINof(3, flist).ToList();
            var v4 = me.usp_GetActelRecordsINof(4, flist).ToList();
            
            //Records Info
            var v5 = me.usp_GetInfoNewRecordsAdded(flist, 1).FirstOrDefault();
            var v6 = me.usp_GetInfoNewRecordsAdded(flist, 2).FirstOrDefault();
            var v7 = me.usp_GetInfoNewRecordsAdded(flist, 3).FirstOrDefault();
            var v8 = me.usp_GetInfoNewRecordsAdded(flist, 4).FirstOrDefault();

            //grid first
            var v9 = me.usp_GetInfoNewRecordsAdded(flist, 1).FirstOrDefault();
            var v10 = me.usp_GetInfoTotalResumesSubmitted(flist, int.Parse(reqType)).ToList();

            //Weekly Report
            var v11 = me.usp_GetWeeklyReportInfo(flist).OrderBy(x => x.Name).ToList();
            var v12 = me.usp_GetWeeklyRecordSummary(flist).ToList();
            var v13 = me.usp_GetWeeklyFileImportSummary(flist).ToList();


            var obj = new AdminViewModel.IndexItems
            {
                dates=List,
                EmailFound= v1,
                EmailNotFound = v2,
                DuplicatesEmails = v3,
                TotalRecords=v4,
                selecteddate=flist,
                MasterListDateWise=v9,
                NewMasterListToday=v5,
                NewNoEmailMasterList = v6,
                TotalMasterList = v7,
                TotalNoEmailMasterList = v8,
                ResumesSubmitted = v10,
                ResumeActionType = reqType,
                weeklyReports = v11,
                recordSummary = v12,
                fileimport = v13
            };
            return View(obj);
        }

        [HttpPost]
        public ActionResult Index(string date)
        {
            TempData["Date"] = date;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Action(string date, string reqtype)
        {
            TempData["Date"] = date;
            TempData["reqType"] = reqtype;
            return RedirectToAction("Index");
        }

        public ActionResult GetOpportunityDashBoard()
        {
            return View();
        }

        #endregion


        #region JSON Results

        [HttpPost]
        public JsonResult GetCandidateDetail(int userid, string date, int reqType)
        {
            var list = me.usp_GetStatusInfoByUserId(date, reqType, userid).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllFilesDetail()
        {
            var list = me.usp_GetAllFileDetails().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOppotunities(int CompanyId)
        {
            var list = me.usp_GetOpportuntiesOnCompany(CompanyId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCompanies()
        {
            var list = me.usp_GetAllCompany().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSearchStringAndRating()
        {
            var list = me.usp_GetSearchStringAndRating(null).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpportunityByDate(string date, string searchkey)
        {
            var list = me.usp_GetOpportunityByDate(date,searchkey).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpportunitySearchStringByDate(string date)
        {
            var list = me.usp_GetOpportunitySearchStringByDate(date).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewRecordsInfoByImportedSource(string date)
        {
            var list = me.usp_GetNewRecordsInfoByImportedSource(date).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOpportunityWeeklyReport(string date)
        {
            var list = me.usp_GetWeeklyOpportunitySummary(date).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
