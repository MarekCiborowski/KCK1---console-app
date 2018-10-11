using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DatabaseLayer.Models
{
    [Table("AccountSurvey")]
    public class AccountSurvey
    {
        [Key]
        public int AccountSurveyID { get; set; }
        
        [ForeignKey("Account")]
        public int AccountID { get; set; }

        [ForeignKey("SurveyID")]
        public int SurveyID { get; set; }
    }
}
