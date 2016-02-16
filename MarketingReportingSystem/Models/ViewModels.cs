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

        public class Selection
        {
            public string Date { get; set; }
            public string SearchString { get; set; }
        }

        public class SubmitterConsultant
        {
            public int statusid { get; set; }
            public string Comment { get; set; }
            public int Rating { get; set; }
            public int MasterID { get; set; }

            public string SearchKey { get; set; }
            public string Date { get; set; }
        }

        public class SearchKeyRating
        {
            public string DateKey { get; set; }
            public string SearchKey { get; set; }
            public int Rating { get; set; }
        }

        public enum ResumeAction
        {
            Submitted=1,
            Connected,
            Called
        }
    }
}