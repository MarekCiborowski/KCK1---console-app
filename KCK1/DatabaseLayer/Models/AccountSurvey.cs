﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DatabaseLayer.Models
{
    //Tabela zapisująca osoby które zagłosowały w danej ankiecie
    [Table("AccountSurvey")]
    public class AccountSurvey
    {
        [Key]
        public int AccountSurveyID { get; set; }
        
        [ForeignKey("account")]
        public int AccountID { get; set; }
        public Account account { get; set; }

        [ForeignKey("survey")]
        public int SurveyID { get; set; }
        public Survey survey { get; set; }
        public bool IsAuthor { get; set; } = false;
        

        //autor nie głosuje w swojej ankiecie
    }
}
