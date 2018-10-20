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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountID { get; set; }

        [Required]
        public virtual PersonData personData { get; set; }

        public string email { get; set; }
        public string nickname { get; set; }
        
        public int followers { get; set; } = 0; //to Roman wymyślił
        [Required]
        public virtual UserSecurity userSecurity { get; set; }
        public ICollection<AccountSurvey> accountSurvey { get; set; }
        
        public ICollection<FollowedUsers> followedUsers { get; set; }
    }
}
