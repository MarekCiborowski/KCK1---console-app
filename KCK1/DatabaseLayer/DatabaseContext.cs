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

            //modelBuilder.Conventions.Add<OneToOneConstraintIntroductionConvention>();
            //modelBuilder.Conventions.Add<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Add<ManyToManyCascadeDeleteConvention>();
        }



    }
}
