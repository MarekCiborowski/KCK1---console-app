using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;

namespace RepositoryLayer.Repositories
{
    public class AccountRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Account GetAccount(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.accounts.FirstOrDefault(a => a.AccountID == id);
        }

        public void AddAccount(Account account)
        {
            db.accounts.Add(account);
            db.SaveChanges();
        }

        public Account EditAccount(Account editedAccount)
        {
            Account account = db.accounts.Find(editedAccount.AccountID);
            account.Email = editedAccount.Email;
            account.Nickname = editedAccount.Nickname;
            account.userSecurity = editedAccount.userSecurity;
            db.SaveChanges();
            return account;
        }

        
        public void RemoveAccount(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Account account = db.accounts.Find(id);

            RemovePersonData(id);

            RemoveUserSecurity(id);

            foreach (AccountSurvey accountSurvey in account.AccountSurvey)
            {
                AccountSurveyRepository asr = new AccountSurveyRepository();
                asr.RemoveAccountSurvey(accountSurvey.AccountSurveyID);
            }

            foreach (FollowedUsers followedUsers in db.followedUsers)
            {
                if (followedUsers.FollowedUsersID == id)
                    RemoveFollowedUsers(id);
            }




            db.accounts.Remove(account);
            db.SaveChanges();
        }
        
        public int GetQuantityOfFollowersByID(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Account account = db.accounts.Find(id);
            return account.Followers;
        }
    }
}
