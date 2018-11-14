using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    public class UserSecurity
    {
        [Key, ForeignKey("account")]
        public int userSecurityID { get; set; }
        [Required]
        public virtual Account account { get; set; }

        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }

        
    }
}
