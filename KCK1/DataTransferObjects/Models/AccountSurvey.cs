using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataTransferObjects.Models
{
    //Tabela zapisująca osoby które zagłosowały w danej ankiecie
    [Table("AccountSurvey")]
    public class AccountSurvey
    {
        [Key]
        public int accountSurveyID { get; set; }
        
        [ForeignKey("account")]
        public int accountID { get; set; }
        
        public Account account { get; set; }

        [ForeignKey("survey")]
        public int surveyID { get; set; }
        
        public Survey survey { get; set; }
        public bool isAuthor { get; set; } = false;
        

        //autor nie głosuje w swojej ankiecie
    }
}
