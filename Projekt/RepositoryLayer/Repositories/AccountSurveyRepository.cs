using DatabaseLayer;
using DataTransferObjects.Models;
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

        public AccountSurvey CreateAccountSurvey(
           int _accountID,
           Account _account,
           int _surveyID,
           Survey _survey,
           bool _isAuthor)
        {
            return new AccountSurvey
            {
                accountID = _accountID,
                account = _account,
                surveyID = _surveyID,
                survey = _survey,
                isAuthor = _isAuthor
            };
        }

        public AccountSurvey GetAccountSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.accountsSurveys.FirstOrDefault(a => a.accountID == id);
        }

        public void AddAccountSurvey(int _accountID, int _surveyID)
        {
            AccountSurvey accountSurvey = new AccountSurvey
            {
                accountID = _accountID,
                surveyID = _surveyID,
                isAuthor = false
            };
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.accountsSurveys.Add(accountSurvey);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }
 
        public void RemoveAccountSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    AccountSurvey accountSurvey = db.accountsSurveys.Find(id);

                    db.accountsSurveys.Remove(accountSurvey);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }
    }
}
