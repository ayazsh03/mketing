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
        private MRS.Data.Model.MarketingEntities me = new MRS.Data.Model.MarketingEntities();

        public ActionResult Index()
        {
            var list = me.Masterlists.Where(x => x.IsSentToAccountMgr == false).ToList();
            return View(list);
        }


        public ActionResult Submit(ViewModels.Acknoledgement ak)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            var user = me.Masterlists.Where(x => ak.MasterId == x.MasterID).FirstOrDefault();
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
