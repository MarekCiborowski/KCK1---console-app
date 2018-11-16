using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountID { get; set; }

        [Required]
        public virtual PersonData personData { get; set; }

        [Required(ErrorMessage ="Email required")]
        [Display(Name ="Email")]
        public string email { get; set; }
        [Display(Name ="Nickname")]
        [Required(ErrorMessage ="Nickname required")]
        public string nickname { get; set; }
        [Required]
        public virtual UserSecurity userSecurity { get; set; }
        public virtual ICollection<AccountSurvey> accountSurvey { get; set; }
        
        public ICollection<Account> followedUsers { get; set; }
        public ICollection<Account> followingUsers { get; set; }
        public ICollection<Vote> votes { get; set; } //oddane przez daną osobę głosy
    }
}
