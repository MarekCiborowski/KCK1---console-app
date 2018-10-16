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
        public int CategoryID { get; set; }
        public bool CanAddOwnAnswer { get; set; }
        public bool IsSingleChoice { get; set; }

        //jeżeli true, to w tabeli Votes będzie zapisane kto głosował
        public bool IsAnonymous { get; set; } 
        
        public ICollection<Question> Question { get; set; }
    }
}
