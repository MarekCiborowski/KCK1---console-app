using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using RepositoryLayer.Repositories;
using DatabaseLayer.Models;

namespace Surveys.Views
{
    public class CreateAccount
    {
        public static void Create()
        {
            ConsoleKeyInfo key;
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());

            Console.SetCursorPosition(30, 23);
            Console.Write("Login: ");
            string login = "";
            login = Console.ReadLine();

            Console.SetCursorPosition(30, 24);
            Console.Write("Password: ");
            string password = "";
            do
            {
                key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            Console.SetCursorPosition(30, 25);
            Console.Write("Nick: ");
            string nickname = "";
            nickname = Console.ReadLine();

            Console.SetCursorPosition(30, 26);
            Console.Write("Email: ");
            string email = "";
            email = Console.ReadLine();

            Console.SetCursorPosition(30, 27);
            Console.Write("Address: ");
            string address = "";
            address = Console.ReadLine();

            Console.SetCursorPosition(30, 28);
            Console.Write("City: ");
            string city = "";
            city = Console.ReadLine();

            Console.SetCursorPosition(30, 29);
            Console.Write("Zipcode: ");

            string zipcode = Console.ReadLine();

            int valueInt = 0;
            bool isNumeric = int.TryParse("123", out valueInt);
            if (!isNumeric)
                while (!isNumeric)
                {
                    Configuration.setConsoleSize();
                    Console.WriteLine(ArtAscii.GetMainTitleString());
                    Console.SetCursorPosition(30, 29);
                    Console.Write("Enter again zipcode: ");
                    zipcode = Console.ReadLine();
                    isNumeric = int.TryParse("123", out valueInt);
                }

            Console.SetCursorPosition(30, 30);
            Console.Write("State: ");
            string state = "";
            state = Console.ReadLine();

            Console.SetCursorPosition(30, 31);
            Console.Write("Country: ");
            string country = "";
            country = Console.ReadLine();

            PersonData personData = new PersonData();
            personData.address = address;
            personData.city = city;
            personData.country = country;
            personData.state = state;
            personData.zipcode = valueInt;

            UserSecurity userSecurity = new UserSecurity();
            userSecurity.login = login;
            userSecurity.password = password;

            AccountRepository accountRepo = new AccountRepository();
            Account account = accountRepo.CreateAccount(personData, email, nickname, userSecurity);

            accountRepo.AddAccount(account);

            Program.Start();
        }
    }
}
