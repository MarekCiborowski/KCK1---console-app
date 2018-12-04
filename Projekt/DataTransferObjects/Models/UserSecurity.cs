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
        [Display(Name ="Login")]
        [Required(ErrorMessage ="Login required")]
        public string login { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage ="Password required")]
        public string password { get; set; }

        
    }
}
