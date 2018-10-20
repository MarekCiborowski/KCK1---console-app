using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Models
{
    public class Followed
    {
        [Key, Column(Order = 1), ForeignKey("Follower")]
        public int FollowerUserId { get; set; }
        [Key, Column(Order = 2), ForeignKey("Following")]
        public int FollowUserId { get; set; }

        

        [InverseProperty("FollowedUsers")]  
        public virtual Account Follower { get; set; }
        [InverseProperty("FollowingUsers")]  
        public virtual Account Following { get; set; }
    }
}
