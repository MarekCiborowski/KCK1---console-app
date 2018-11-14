using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    public class PersonData
    {
        [Key, ForeignKey("account")]
        public int personDataID { get; set; }
        [Required]
        public virtual Account account { get; set; }
        [Required]

        public string address { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string zipcode { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string country { get; set; }

    }
}
