﻿using DatabaseLayer;
using DatabaseLayer.Models;
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
            db.userSecurities.Add(userSecurity);
            db.SaveChanges();
        }

        public void EditUserSecurity(UserSecurity editedUserSecurity)
        {
            editedUserSecurity.password = hashPassword(editedUserSecurity.password);
            db.Entry(editedUserSecurity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveUserSecurity(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            UserSecurity userSecurity = db.userSecurities.Find(id);
            db.userSecurities.Remove(userSecurity);
            db.SaveChanges();
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