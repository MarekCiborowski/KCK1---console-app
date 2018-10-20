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

        public Account GetAccount(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.accounts.FirstOrDefault(a => a.AccountID == id);
        }
        public Account GetAccount(string login, string password)
        {
            string hashedPassword = hashPassword(password);
            UserSecurity userSecurity = db.userSecurity.Include(t => t.Account).FirstOrDefault(t => t.Login == login && t.Password == hashedPassword);
            if (userSecurity == null)
                throw new ArgumentNullException("Wrong password or login");
            return db.accounts.Find(userSecurity.Account);
        }
        

        public void AddAccount(Account account, PersonData personData, UserSecurity userSecurity )
        {
            account.userSecurity = userSecurity;
            account.personData = personData;
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
    }
}
