using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class AccountSurveyRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public AccountSurvey GetAccountSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.accountsSurveys.FirstOrDefault(a => a.accountID == id);
        }

        public void AddAccountSurvey(AccountSurvey accountSurvey)
        {
            db.accountsSurveys.Add(accountSurvey);
            db.SaveChanges();
        }
 
        public void RemoveAccountSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            AccountSurvey accountSurvey = db.accountsSurveys.Find(id);

            db.accountsSurveys.Remove(accountSurvey);
            db.SaveChanges();
        }
    }
}
