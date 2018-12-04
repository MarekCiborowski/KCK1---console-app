using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Survey")]
    public class Survey
    {
        [Key]
        [Display(Name ="Survey ID")]
        public int surveyID { get; set; }
        [Display(Name ="Survey's title")]
        [Required(ErrorMessage ="Title required")]
        public string title { get; set; }
        [Display(Name ="Survey's description")]
        [Required(ErrorMessage = "Description required")]

        public string description { get; set; }
        public bool isAnonymous { get; set; }

        public ICollection<Question> question { get; set; }
        public virtual ICollection<AccountSurvey> accountSurvey { get; set; } //Osoby które zagłosowały w danej ankiecie lub autorzy
    }
}
