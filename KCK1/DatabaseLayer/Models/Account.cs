using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int AccountID { get; set; }
        
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Followers { get; set; } = 0; //to Roman wymyślił
        public virtual UserSecurity UserSecurity { get; set; }
        public ICollection<AccountSurvey> AccountSurvey { get; set; }
        
        public ICollection<FollowedUsers> FollowedUsers { get; set; }
    }
}
