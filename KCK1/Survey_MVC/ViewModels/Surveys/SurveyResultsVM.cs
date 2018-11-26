using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class SurveyResultsVM
    {
        public string title { get; set; }
        public string authorNickname { get; set; }
        public int authorID { get; set; }
        public List<QuestionResultsVM> questions { get; set; }
        public int numberOfVoters { get; set; }
    }
}