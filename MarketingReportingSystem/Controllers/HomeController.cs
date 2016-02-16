using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MarketingReportingSystem.Models;
namespace MarketingReportingSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private MRS.Data.Model.Entities me = new MRS.Data.Model.Entities();

        public ActionResult Index()
        {
            //var list = me.Masterlists.Where(x => x.IsSentToAccountMgr == false).ToList();
            //return View(list);
            var obj = Tuple.Create<string, string,string>(
                TempData["Date"]==null?"": TempData["Date"].ToString(),
                TempData["SearchKey"] == null ? "" : TempData["SearchKey"].ToString(),
                TempData["SearchKeyRating"] == null ? "" : TempData["SearchKeyRating"].ToString()
                );
            return View(obj);
        }

        public JsonResult GetData(MarketingReportingSystem.Models.ViewModels.Selection sc)
        {
            //var dtTime = DateTime.Now;
            //dtTime = new DateTime(dtTime.Year,dtTime.Month, dtTime.Day,0,0,0).AddDays(-2);
            //var list = me.MasterLists.Where(x => x.UpdateDate > dtTime && x.LoginName == null).ToList();
            //var dlist = list.Where(x => x.UpdateDate.ToString("MM-dd-yyyy").Contains(sc.Date)).ToList();
            //var slist = dlist.Where(x => x.SearchString == (sc.SearchString == null ? "" : sc.SearchString)).ToList();

            var datedata = sc.Date == null ? "" : sc.Date;
            var searchdata = sc.SearchString == null ? "" : HttpUtility.HtmlDecode(sc.SearchString);
            var dlist = me.GetDataOnDateAndSearchKey(datedata, searchdata).ToList(); 
            JsonResult jsonResult = Json(dlist, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public PartialViewResult ComboDate()
        {
            var data = TempData["Date"] ?? string.Empty;
            //var dtTime = DateTime.Now;
            //dtTime = new DateTime(dtTime.Year,dtTime.Month, dtTime.Day,0,0,0).AddDays(-2);

           // var list = me.MasterLists.Where(x => x.UpdateDate > dtTime && x.LoginName == null).ToList().OrderByDescending(x => x.UpdateDate).Select(x => x.UpdateDate.ToString("MM-dd-yyyy"));
           // var flist = list.Distinct().ToList();
            var flist = me.GetDateString().ToList();
            var obj = Tuple.Create<dynamic, string, string>(flist, "Date", data.ToString());

            return PartialView("ComboBox", obj);
        }

        public PartialViewResult ComboSearchString()
        {
            var data = TempData["Date"]!=null? TempData["Date"].ToString() : string.Empty;

            var dtTime = DateTime.Now;
            dtTime = new DateTime(dtTime.Year, dtTime.Month, dtTime.Day, 0, 0, 0).AddDays(-2);


            if (data == "")
            {
                data = dtTime.ToString("MM-dd-yyyy");
            }
           

           // var list = me.Masterlists.Where(x => x.UpdateDate > dtTime && x.LoginName==null).OrderBy(x=> x.UpdateDate).ToList();
           // var flist = list.Where(x=> x.UpdateDate.ToString("MM-dd-yyyy").Contains(data))
           //     .Select(x => x.SearchString).Distinct().ToList();
           /// me.
            var flist = me.GetSearchString(data).ToList<string>();
            var obj = Tuple.Create<dynamic, string, string>(flist, "SearchString", string.Empty);
            return PartialView("ComboBox",obj);
        }


        public JsonResult SaveMasterListReviews(ViewModels.SubmitterConsultant sc)
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var isFlag = false;
            try
            {
                //var masterdata = me.MasterLists.Where(x => sc.MasterID == x.MasterID).FirstOrDefault();
                //if (masterdata != null)
                //{
                //    masterdata.comment = sc.Comment;
                //    masterdata.statusid = sc.statusid;
                //    masterdata.Rating = sc.Rating;
                //    masterdata.IsSentToAccountMgr = true;
                //    masterdata.UserId = int.Parse(ticket.Name.Substring(0, ticket.Name.IndexOf(";")));
                //    me.SaveChanges();
                //}

                //var mastersr = me.MasterSkillRatings.Where(x => sc.MasterID == x.MasterID && x.SearchString== HttpUtility.HtmlDecode(sc.SearchKey))
                //    .ToList().Where(x=> x.UpdateDate == sc.Date)
                //if (mastersr != null)
                //{
                //    mastersr.Rating = sc.Rating;
                //    masterdata.UserId = int.Parse(ticket.Name.Substring(0, ticket.Name.IndexOf(";")));
                //    me.SaveChanges();
                //}

                me.usp_UpdateSkillsRating(sc.statusid, sc.Comment, sc.Rating, sc.MasterID, sc.SearchKey, sc.Date, int.Parse(ticket.Name.Substring(0, ticket.Name.IndexOf(";"))));


                isFlag = true;
            }
            catch (Exception ex)
            {
                isFlag = false;

                System.IO.File.AppendAllText("Error.log", ex.Data.ToString());
            }


            return Json(isFlag, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveSearchRating(ViewModels.SearchKeyRating sc)
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var isFlag = false;
            try
            {
                int Userid = int.Parse(ticket.Name.Substring(0, ticket.Name.IndexOf(";")));
                var searchKey =   sc.SearchKey == null ? "" : HttpUtility.HtmlDecode(sc.SearchKey);
                me.usp_UpdateOrGetSearchKeyRating(sc.DateKey, searchKey, sc.Rating, Userid, false);
                isFlag = true;
            }
            catch (Exception ex)
            {
                isFlag = false;

                System.IO.File.AppendAllText("Error.log", ex.Data.ToString());
            }


            return Json(isFlag, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DateAndSearch()
        {
           var dtstrTime = me.GetDateString().FirstOrDefault();
           string data=string.Empty;

           if (TempData["Date"] == null)
           {
               data = dtstrTime;
           }
           else
           {
               data = TempData["Date"].ToString();
           }

            TempData["Date"] = data;

            return View("DateAndSearch",(object)data);
        }

        [HttpPost]
        public ActionResult DateSelection(ViewModels.Selection sc)
        {
            TempData["Date"] = sc.Date;
            return RedirectToAction("DateAndSearch");
        }

        [HttpPost]
        public ActionResult SearchStringSelection(ViewModels.Selection sc)
        {
            TempData["Date"] = sc.Date;
            TempData["SearchKey"] = sc.SearchString;
            TempData["SearchKeyRating"] = me.usp_UpdateOrGetSearchKeyRating(sc.Date, sc.SearchString, null, null, true).FirstOrDefault();
            return Redirect("index");
        }


        public ActionResult Submit(ViewModels.Acknoledgement ak)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var user = me.MasterLists.Where(x => ak.MasterId == x.MasterID).FirstOrDefault();
            if (user!=null)
            {
                user.comment = ak.Comment;
                user.statusid = ak.Status == true ? 1 : 0;
                user.IsSentToAccountMgr = true;
                user.UserId = int.Parse(ticket.Name.Substring(0, ticket.Name.IndexOf(";")));
                me.SaveChanges(); 
            }

            return RedirectToAction("Index");

        }
    }
}
