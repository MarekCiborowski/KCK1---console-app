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
        [Key, ForeignKey("Account")]
        public int UserSecurityID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Account Account { get; set; }
    }
}
