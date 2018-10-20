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

        public Survey CreateAccount(
            PersonData _persondata, 
            string _email, 
            string _nickname, 
            UserSecurity _usersecurity)
        {
            return new Survey
            {
                personData = _persondata,
                email = _email,
                nickname = _nickname,
                userSecurity = _usersecurity
            };
        }

        public Survey GetAccount(int? id)
        {
            return accountR.GetAccount(id);
        }

        public void AddAccount(Survey account, PersonData personData, UserSecurity userSecurity)
        {
            accountR.AddAccount(account, personData, userSecurity);
        }

        public void DeleteAccount(int? id)
        {
            accountR.RemoveAccount(id);
        }

        public void EditAccount(Survey account)
        {
            accountR.EditAccount(account);
        }
    }
}
