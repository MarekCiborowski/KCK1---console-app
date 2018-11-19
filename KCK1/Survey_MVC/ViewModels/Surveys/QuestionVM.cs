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
        public string questionValue { get; set; }
        public bool isSingleChoice { get; set; }
        public bool canAddOwnAnswers { get; set; }
        public ICollection<AnswerVM> answers { get; set; } = new Collection<AnswerVM>();
        public string newAnswer { get; set; }
    }
}