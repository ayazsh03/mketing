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

            public string fromdate { get; set; }
            public string todate { get; set; }
            public int ReqType { get; set; } 
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


        public class MasterData
        {
            public int MasterID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email1 { get; set; }
            public string PrimaryPhone { get; set; }
            public string Url { get; set; }
            public string Location { get; set; }
            public string CurrentJob { get; set; }
            public string Company { get; set; }
            public string comment { get; set; }
        }
    }
}