using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLayer.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public string AnswerValue { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
       
    }
}
