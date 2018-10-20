using RepositoryLayer.Repositories;
using DatabaseLayer.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Surveys
{
    class Program
    {
        static void Main(string[] args)
        {
            AccountRepository accountRepository = new AccountRepository();

            //PersonData personData = new PersonData();
            //personData.address = "address";
            //personData.city = "city";
            //personData.country = "country";
            //personData.state = "state";
            //personData.zipcode = 48;

            //UserSecurity userSecurity = new UserSecurity();
            //userSecurity.login = "login";
            //userSecurity.password = "password";

            //Account account = accountRepository.CreateAccount(personData, "kuc@gmail.com", "kuc", userSecurity);
            //accountSRepository.AddAccount(account);
            accountRepository.RemoveAccount(1);
            

        }
    }
}
