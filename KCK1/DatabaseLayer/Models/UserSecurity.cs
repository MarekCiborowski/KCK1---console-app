using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class UserSecurity
    {
        [Key, ForeignKey("account")]
        public int userSecurityID { get; set; }
        public virtual Survey account { get; set; }

        public string login { get; set; }
        public string password { get; set; }

        
    }
}
