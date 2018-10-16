using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class Votes
    {
        [Key]
        public int VotesID { get; set; }
        [ForeignKey("answer")]
        public int AnswerID { get; set; }
        public Answer answer { get; set; }

        [ForeignKey("account")]
        //jeżeli ankieta anonimowa to null
        public int? AccountID { get; set; } = null;
        public Account account { get; set; }



    }
}
