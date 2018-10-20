using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class PersonData
    {
        [Key, ForeignKey("account")]
        public int personDataID { get; set; }

        public virtual Survey account { get; set; }

        public string address { get; set; }
        public string city { get; set; }
        public int zipcode { get; set; }
        public string state { get; set; }
        public string country { get; set; }

    }
}
