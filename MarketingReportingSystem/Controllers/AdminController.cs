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
                ResumeActionType = reqType
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


    }
}
