﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class QuestionResultsVM
    {
        public string questionValue { get; set; }
        public bool isAnonymous { get; set; }
        public int questionID { get; set; }
        public List<AnswerVotersVM> answers {get;set;}
    }
}