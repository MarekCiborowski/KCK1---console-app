using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    public class PersonData
    {
        [Key, ForeignKey("account")]
        public int personDataID { get; set; }
        [Required]
        public virtual Account account { get; set; }
        [Display(Name ="Address")]
        [Required (ErrorMessage ="Address Required")]

        public string address { get; set; }
        [Display(Name ="City")]
        [Required (ErrorMessage ="City required")]
        public string city { get; set; }
        [Display(Name ="Zipcode")]
        [Required (ErrorMessage ="Zipcode required")]
        public string zipcode { get; set; }
        [Display(Name ="State")]
        [Required (ErrorMessage ="State required")]
        public string state { get; set; }
        [Display(Name ="Country")]
        [Required(ErrorMessage ="Country required")]
        public string country { get; set; }
        public bool isProfilePublic { get; set; } = false;
        //inni użytkownicy mogą wyświetlić dane inne niż nickname

    }
}
