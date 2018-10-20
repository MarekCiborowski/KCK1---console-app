using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public bool canAddOwnAnswer { get; set; }
        public bool isSingleChoice { get; set; }

        //jeżeli true, to w tabeli Votes będzie zapisane kto głosował
        public bool isAnonymous { get; set; } 
        
        public ICollection<Question> question { get; set; }
    }
}
