using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Services
{
    public class AccountService
    {
        private AccountRepository accountR = new AccountRepository();

        public Account CreateAccount(
            PersonData persondata, 
            string email, 
            string nickname, 
            UserSecurity usersecurity)
        {
            return new Account
            {
                personData = persondata,
                Email = email,
                Nickname = nickname,
                userSecurity = usersecurity
            };
        }

        public Account GetAccount(int? id)
        {
            return accountR.GetAccount(id);
        }

        public void AddAccount(Account account, PersonData personData, UserSecurity userSecurity)
        {
            accountR.AddAccount(account, personData, userSecurity);
        }

        public void DeleteAccount(int? id)
        {
            accountR.RemoveAccount(id);
        }

        public void EditAccount(Account account)
        {
            accountR.EditAccount(account);
        }
    }
}
