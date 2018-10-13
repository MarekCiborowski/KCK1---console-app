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
        public int SurveyID { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }

        public ICollection<Question> Question { get; set; }
        public ICollection<AccountSurvey> AccountSurvey { get; set; } //Osoby które zagłosowały w danej ankiecie
    }
}
