using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MarketingReportingSystem.Models;
using MRS.Data;
namespace MarketingReportingSystem.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private MRS.Data.Model.MarketingEntities me = new MRS.Data.Model.MarketingEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ViewModels.Login login)
        {
            if(ModelState.IsValid){
                var user = me.Users.Where(m => m.UserName == login.Username && m.password == login.Password).FirstOrDefault();
                if (user==null)
                {
                    ModelState.AddModelError("",  "Username/password is not matching."); 
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user.Id +";" + user.UserName, false);
                    return Redirect("~/Home/Index");

                }
            }
            return View();
        }

    }
}
