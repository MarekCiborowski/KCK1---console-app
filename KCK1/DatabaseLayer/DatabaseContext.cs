using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=SurveysDatabase") { }

        public virtual DbSet<Account> accounts { get; set; }
        public virtual DbSet<AccountSurvey> accountsSurveys { get; set; }
        public virtual DbSet<Answer> answers { get; set; }
        public virtual DbSet<Question> questions { get; set; }
        public virtual DbSet<Survey> surveys { get; set; }


    }
}
