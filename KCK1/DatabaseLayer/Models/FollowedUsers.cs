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
        public int FollowedUsersID { get; set; }
        [ForeignKey("followedUser")]
        public int FollowedUserID { get; set; }
        public Account followedUser { get; set; }
        
    }
}
