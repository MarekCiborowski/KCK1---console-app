using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public virtual DbSet<UserSecurity> userSecurities { get; set; }
        public virtual DbSet<PersonData> personDatas { get; set; }
        public virtual DbSet<Vote> votes { get; set; }

        public virtual DbSet<Category> categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                 .HasMany(x => x.followedUsers).WithMany(x => x.followingUsers)
                 .Map(x => x.ToTable("Followers")
                     .MapLeftKey("UserId")
                     .MapRightKey("FollowerId"));

            modelBuilder.Entity<PersonData>()
                .HasRequired(p => p.account)
                .WithOptional(p => p.personData)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserSecurity>()
                .HasRequired(u => u.account)
                .WithOptional(u => u.userSecurity)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Account>().
                HasMany(p => p.accountSurvey).
                WithRequired(t => t.account).
                HasForeignKey(t => t.accountID);

            modelBuilder.Entity<Survey>().
                HasMany(p => p.accountSurvey).
                WithRequired(t => t.survey).
                HasForeignKey(t => t.surveyID);

            modelBuilder.Entity<Account>().
                HasMany(p => p.votes).
                WithRequired(t => t.account).
                HasForeignKey(t => t.accountID);

            modelBuilder.Entity<Answer>().
                HasMany(p => p.vote).
                WithRequired(t => t.answer).
                HasForeignKey(t => t.answerID);



            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            

            //modelBuilder.Conventions.Add<OneToOneConstraintIntroductionConvention>();
            //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();
        }



    }
}
