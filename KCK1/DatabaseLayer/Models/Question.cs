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
        public int questionID { get; set; }
        public string questionValue { set; get; }
        [ForeignKey("category")]
        public int categoryID { get; set; }
        public Category category { get; set; }

        [ForeignKey("survey")]
        public int surveyID { get; set; }
        [Required]
        public Survey survey { get; set; }
        public ICollection<Answer> answer { get; set; }
    }
}
