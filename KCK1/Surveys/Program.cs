﻿
using DatabaseLayer.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Repositories;
using Terminal.Gui;

namespace Surveys
{
    class Program
    {
        private Account loggedAccount;
        static void cc() { Console.Clear(); }
        static void cr() { Console.ReadKey(); }
        
        static void Main(string[] args)
        {
            Application.Init();
            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Quit", "", () => {
                    Application.RequestStop ();
                })
            }),
        });

            var win = new Window("Hello")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill() - 1
            };

            // Add both menu and win in a single call
            Application.Top.Add(menu, win);
            Application.Run();

        }

        static void SomeTest()
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

            //PersonData personData1 = new PersonData();
            //personData1.address = "address";
            //personData1.city = "city";
            //personData1.country = "country";
            //personData1.state = "state";
            //personData1.zipcode = 48;

            //UserSecurity userSecurity1 = new UserSecurity();
            //userSecurity1.login = "login";
            //userSecurity1.password = "password";

            //Account account1 = accountRepository.CreateAccount(personData1, "kuc1@gmail.com", "kuc1", userSecurity1);

            //int accountId = accountRepository.AddAccount(account);

            //int account1Id = accountRepository.AddAccount(account1);

            //accountRepository.AddFollower(accountId, account1Id);

            accountRepository.RemoveAccount(6);
        }
    }
}
