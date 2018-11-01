using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using RepositoryLayer.Repositories;
using DatabaseLayer.Models;
using Surveys;

namespace Surveys.Views
{
    public class CreateAccount
    {
        public static void Create()
        {
            AccountRepository accountRepo = new AccountRepository();
            PersonDataRepository personDataRepo = new PersonDataRepository();
            int positionX = 30, positionY = 15;
            ConsoleKeyInfo key;
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());

            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Login: ");
            string login = "";
            login = Console.ReadLine();
            if(!accountRepo.IsLoginFree(login))
                while(!accountRepo.IsLoginFree(login))
                {
                    Configuration.CurrentConsoleLineClear(positionX);
                    positionY++;
                    Console.Write("This login is busy. Please try another: ");
                    login = Console.ReadLine();
                }
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Password: ");
            string password = "";
            do
            {
                key = Console.ReadKey(true);
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

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Nick: ");
            string nickname = "";
            nickname = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Email: ");
            string email = "";
            email = Console.ReadLine();
            if (!accountRepo.IsEmailFree(email))
                while (!accountRepo.IsEmailFree(email))
                {
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("This email is busy. Please try another: ");
                    email = Console.ReadLine();
                }

            bool var = IsValidEmail(email);

            if(!var)
                while (!var)
                {
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("Enter again your email: ");
                    email = Console.ReadLine();
                    var = IsValidEmail(email);
                }

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Address: ");
            string address = "";
            address = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("City: ");
            string city = "";
            city = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Zipcode: ");
            string zipcode = "";
            zipcode = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("State: ");
            string state = "";
            state = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Country: ");
            string country = "";
            country = Console.ReadLine();

            PersonData personData = personDataRepo.CreatePersonData(address, city, zipcode, state, country);              
            UserSecurity userSecurity = accountRepo.CreateUserSecurity(login,password);                      
            Account account = accountRepo.CreateAccount(personData, email, nickname, userSecurity);
            accountRepo.AddAccount(account);

            Program.Start("Account was created.");
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var check = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
