﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects.Models
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int questionID { get; set; }
        [Required]
        public string questionValue { set; get; }
        
        public bool canAddOwnAnswer { get; set; }
        public bool isSingleChoice { get; set; }

        [ForeignKey("survey")]
        public int surveyID { get; set; }
        [Required]
        public Survey survey { get; set; }
        public ICollection<Answer> answer { get; set; }
    }
}
