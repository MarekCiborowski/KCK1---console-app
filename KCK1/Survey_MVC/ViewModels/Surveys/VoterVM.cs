using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class VoterVM
    {
        public int voterID { get; set; }
        public string email { get; set; }
        public string nickname { get; set; }
    }
}