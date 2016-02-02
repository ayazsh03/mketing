using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketingReportingSystem.Models
{
    public class ViewModels
    {
        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }

        }

        public class Acknoledgement
        {
            public bool Status { get; set; }
            public string Comment { get; set; }
            public int MasterId { get; set; }
        }
    }
}