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
        public int surveyID { get; set; }
        [Required]
        public string title { get; set; }
        [Required]

        public string description { get; set; }
        public bool isAnonymous { get; set; }

        public ICollection<Question> question { get; set; }
        public ICollection<AccountSurvey> accountSurvey { get; set; } //Osoby które zagłosowały w danej ankiecie lub autorzy
    }
}
