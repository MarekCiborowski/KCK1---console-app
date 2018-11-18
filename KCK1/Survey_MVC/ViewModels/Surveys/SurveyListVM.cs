using DataTransferObjects.Models;
using Survey_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class SurveyListVM:Survey
    {
        public IEnumerable<Survey> surveyList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}