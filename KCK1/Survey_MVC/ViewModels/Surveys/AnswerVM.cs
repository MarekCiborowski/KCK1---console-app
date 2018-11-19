using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class AnswerVM
    {
        public string value { get; set; }
        public bool isChecked { get; set; } = false;
        
    }
}