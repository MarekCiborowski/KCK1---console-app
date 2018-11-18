using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class SurveyToFill
    {
        public int surveyID { get; set; }
        public bool isAnonymous { get; set; }
        public IEnumerable<Question> questions { get; set; }
    }
}