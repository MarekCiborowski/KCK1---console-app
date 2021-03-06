﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataTransferObjects.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public int answerID { get; set; }
        [Required]
        public string answerValue { get; set; }

        [ForeignKey("question")]
        public int questionID { get; set; }
        [Required]
        public Question question { get; set; }
        public ICollection<Vote> vote { get; set; }
       
    }
}
