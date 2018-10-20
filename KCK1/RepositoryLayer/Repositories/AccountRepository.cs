using System;
using System.Collections.Generic;
using System.Data.Entity;
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
