using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using DatabaseLayer.Models;
using DatabaseLayer;
using RepositoryLayer.Repositories;

namespace Surveys.Views
{
    public class SignInView
    {
        public static void SignIn()
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Login: ");

            string login;
            login = Console.ReadLine();

            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Password: ");
            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
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

            AccountRepository accountRepo = new AccountRepository();
            Account account = null;
            account = accountRepo.GetAccount(login, password);
            if (account == null)
                Program.Start("Wrong login or password, try again.");
                
            AfterSignIn.Start(account);

        }
    }
}
