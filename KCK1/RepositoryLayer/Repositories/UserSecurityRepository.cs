using DatabaseLayer;
using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class UserSecurityRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public UserSecurity CreateUserSecurity(
            string _login,
            string _password)
        {
            return new UserSecurity
            {
                login = _login,
                password = hashPassword(_password),
                
            };
        }
        public UserSecurity GetSecurity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.userSecurities.FirstOrDefault(u => u.userSecurityID == id);
        }

        public void AddUserSecurity(UserSecurity userSecurity)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.userSecurities.Add(userSecurity);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void EditUserSecurity(UserSecurity editedUserSecurity)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    editedUserSecurity.password = hashPassword(editedUserSecurity.password);
                    db.Entry(editedUserSecurity).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void RemoveUserSecurity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    UserSecurity userSecurity = db.userSecurities.Find(id);
                    db.userSecurities.Remove(userSecurity);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }


        public bool IsLoginFree(string login)
        {
            UserSecurity userSecurity = db.userSecurities.Include(t => t.account).FirstOrDefault(t => t.login == login);
            if (userSecurity == null)
                return true;
            return false;
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
