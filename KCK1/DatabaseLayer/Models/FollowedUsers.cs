using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class FollowedUsers
    {
        [Key]
        public int followedUsersID { get; set; }
        [ForeignKey("followedUser")]
        public int followedUserID { get; set; }
        public Survey followedUser { get; set; }
        
    }
}
