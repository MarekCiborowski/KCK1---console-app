﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public int AccountID { get; set; }

        [Required]
        public virtual PersonData personData { get; set; }

        public string Email { get; set; }
        public string Nickname { get; set; }
        
        
        [Required]
        public virtual UserSecurity userSecurity { get; set; }
        public ICollection<AccountSurvey> AccountSurvey { get; set; }
        
        public ICollection<Account> FollowedUsers { get; set; }
        public ICollection<Account> FollowingUsers { get; set; }
    }
}
