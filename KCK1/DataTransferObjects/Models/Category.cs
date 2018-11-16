using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public bool canAddOwnAnswer { get; set; }
        public bool isSingleChoice { get; set; }

        
        
        
        public ICollection<Question> question { get; set; }
    }
}
