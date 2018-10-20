using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=SurveysDatabase") { }

        public virtual DbSet<Account> accounts { get; set; }
        public virtual DbSet<AccountSurvey> accountsSurveys { get; set; }
        public virtual DbSet<Answer> answers { get; set; }
        public virtual DbSet<Question> questions { get; set; }
        public virtual DbSet<Survey> surveys { get; set; }
        public virtual DbSet<UserSecurity> userSecuritys { get; set; }
        public virtual DbSet<PersonData> personDatas { get; set; }
        public virtual DbSet<Vote> votes { get; set; }
        public virtual DbSet<FollowedUsers> followedUsers { get; set; }
        public virtual DbSet<Category> categorys { get; set; }



    }
}
