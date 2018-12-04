using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    public class Vote
    {
        [Key]
        public int voteID { get; set; }
        [ForeignKey("answer")]
        public int answerID { get; set; }
        
        public Answer answer { get; set; }

        [ForeignKey("account")]
        //jeżeli ankieta anonimowa to null
        public int? accountID { get; set; } = null;

        public Account account { get; set; } //jak będzie null to wyswietlimy jako usuniete konto
    }
}
