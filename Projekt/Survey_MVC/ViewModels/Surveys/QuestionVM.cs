using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class QuestionVM
    {
        public int questionID { get; set; }
        public string questionValue { get; set; }
        public bool isSingleChoice { get; set; }
        public bool canAddOwnAnswers { get; set; }
        public int? selectedAnswersID { get; set; } = null; //for single choice
        public List<AnswerVM> answers { get; set; } = new List<AnswerVM>();
        public string newAnswer { get; set; }
    }
}