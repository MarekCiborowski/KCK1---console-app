using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [Table("Survey")]
    public class Survey
    {
        [Key]
        public int surveyID { get; set; }
        public string title { get; set; }
        
        public string description { get; set; }
        public bool isAnonymous { get; set; }

        public ICollection<Question> question { get; set; }
        public ICollection<AccountSurvey> accountSurvey { get; set; } //Osoby które zagłosowały w danej ankiecie
    }
}
