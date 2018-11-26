using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class AnswerVotersVM
    {
        public int answerID { get; set; }
        public string answerValue { get; set; }
        public int numberOfVotes { get; set; }
        public List<VoterVM> voters { get; set; }
    }
}