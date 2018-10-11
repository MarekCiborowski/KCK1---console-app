using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionValue { set; get; }
        public bool OptionOfAddingAnswers { get; set; }
        public bool IsSingleChoice { get; set; }

        [ForeignKey("Survey")]
        public int SurveyID { get; set; }
    }
}
