using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class CreateSurveyVM
    {
        [Display(Name ="Title")]
        [Required(ErrorMessage ="Title required")]
        public string title { get; set; }

        [Display(Name ="Description")]
        [Required(ErrorMessage = "Description required")]
        public string description { get; set; }

        [Display(Name ="Is anonymous?")]
        public bool isAnonymous { get; set; }

        public List<NewQuestion> questions { get; set; } = new List<NewQuestion>();

        public NewQuestion newQuestion { get; set; }


        public class NewQuestion
        {
            [Display(Name = "Question")]
            public string questionValue { get; set; }

            [Display(Name = "Is a single choice?")]
            public bool isSingleChoice { get; set; }

            [Display(Name = "Can user add his/her own answers?")]
            public bool canAddOwnAnswers { get; set; }

            public List<string> answers { get; set; } = new List<string>();

            public string newAnswer { get; set; }
        }
    }
}