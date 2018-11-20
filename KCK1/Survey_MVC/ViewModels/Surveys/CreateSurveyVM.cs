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
        [Display(Name ="Survey's title")]
        [Required(ErrorMessage ="Title required")]
        public string title { get; set; }

        [Display(Name ="Survey's description")]
        [Required(ErrorMessage = "Description required")]
        public string description { get; set; }

        [Display(Name ="Is anonymous?")]
        public bool isAnonymous { get; set; }

        public List<QuestionVM> questions { get; set; }
    }
}