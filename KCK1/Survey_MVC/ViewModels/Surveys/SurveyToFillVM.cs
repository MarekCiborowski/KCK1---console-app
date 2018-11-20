using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class SurveyToFillVM
    {
        public int surveyID { get; set; }
        public bool isAnonymous { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string authorNickname { get; set; }
        public List<QuestionVM> questions { get; set; } = new List<QuestionVM>();
       
    }
}