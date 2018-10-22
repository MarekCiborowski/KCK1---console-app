using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;

namespace RepositoryLayer.Repositories
{
    public class AccountRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Account CreateAccount(
            PersonData _persondata,
            string _email,
            string _nickname,
            UserSecurity _usersecurity)
        {
            return new Account
            {
                personData = _persondata,
                email = _email,
                nickname = _nickname,
                userSecurity = _usersecurity
            };
        }
        public Account GetAccount(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.accounts.FirstOrDefault(a => a.accountID == id);
        }
        public Account GetAccount(string login, string password)
        {
            string hashedPassword = hashPassword(password);
            UserSecurity userSecurity = db.userSecuritys.Include(t => t.account).FirstOrDefault(t => t.login == login && t.password == hashedPassword);
            if (userSecurity == null)
                throw new ArgumentNullException("Wrong password or login");
            return db.accounts.Find(userSecurity.account);
        }
        

        public void AddAccount(Account account/*, PersonData personData, UserSecurity userSecurity */)
        {
            //account.userSecurity = userSecurity;
            //account.personData = personData;
            db.accounts.Add(account);
            db.SaveChanges();
        }

        public void EditAccount(Account editedAccount)
        {
            
            db.Entry(editedAccount).State = EntityState.Modified;
            db.SaveChanges();
            
        }

        
        public void RemoveAccount(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Account account = db.accounts.Find(id);

            db.personDatas.Remove(account.personData);
            db.userSecuritys.Remove(account.userSecurity);
            db.accounts.Remove(account);
            db.SaveChanges();
        }
        
        public int GetQuantityOfFollowersByID(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Account account = db.accounts.Find(id);
            return account.followers;
        }
        private string hashPassword(string password)
        {
            MD5 hash = MD5.Create();

            return GetMd5Hash(hash, password);
        }

        private static string GetMd5Hash(MD5 hash, string input)
        {
            // Konwertowanie stringa do tablicy bajtów i wyliczanie hashowania
            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Stworzenie StringBuildera do przechowywania bajtów i stworzenie stringa
            StringBuilder sBuilder = new StringBuilder();

            // Każdy bajt jest haszowany i formatowany do postaci ciągu szesnastkowego
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Zwracanie zahaszowanego stringa
            return sBuilder.ToString();
        }

        public List<Account> GetFollowedAccounts(int? id)
        {
            if(id == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.Find(id);

            return account.followedUsers.ToList();
        }

        // Returns all the surveys that this particular account created/took part in.
        // Returns null if none are present.
        public List<Survey> GetFollowedAccountSurveys(int? idFollowedAccount)
        {
            if(idFollowedAccount == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.Find(idFollowedAccount);
            AccountSurvey accountSurvey = db.accountsSurveys.Find();


            List<Survey> surveys = new List<Survey>();
            foreach (AccountSurvey accSurvey in account.accountSurvey)
            {
                Survey s = db.surveys.Find(accSurvey.surveyID);
                if(s != null)
                    surveys.Add(s);
            }

            if (!surveys.Any())
                return null;
            else
                return surveys;
        }

        // Returns all the survey that particular account is an author of.
        // Null is returned if none are present.
        public List<Survey> GetAccountAuthorSurveys(int? id)
        {

            if (id == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.Find(id);
            AccountSurvey accountSurvey = db.accountsSurveys.Find();


            List<Survey> surveys = new List<Survey>();
            foreach (AccountSurvey accSurvey in account.accountSurvey)
            {
                if (accSurvey.isAuthor)
                {
                    Survey s = db.surveys.Find(accSurvey.surveyID);
                    if (s != null)
                        surveys.Add(s);
                }
            }

            if (!surveys.Any())
                return null;
            else
                return surveys;
        }

        // Returns all the filled surveys that particular account has.
        // Null if none are present.
        public List<Survey> GetAccountFilledSurveys(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.Find(id);
            AccountSurvey accountSurvey = db.accountsSurveys.Find();


            List<Survey> surveys = new List<Survey>();
            foreach (AccountSurvey accSurvey in account.accountSurvey)
            {
                if (!accSurvey.isAuthor)
                {
                    Survey s = db.surveys.Find(accSurvey.surveyID);
                    if (s != null)
                        surveys.Add(s);
                }
            }

            if (!surveys.Any())
                return null;
            else
                return surveys;
        }

        public bool DidFilledSurvey(int? accountID, int? surveyID)
        {
            if(accountID == null || surveyID == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.Find(accountID);

            Survey survey = db.surveys.Find(surveyID);
            List<AccountSurvey> accountSurvey = survey.accountSurvey.ToList();

            foreach (AccountSurvey accSurv in accountSurvey)
            {
                if (accSurv.accountID == account.accountID && !accSurv.isAuthor)
                    return true;
            }

            return false;
        }
    }
}
