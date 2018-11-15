using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;

using DataTransferObjects.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;

namespace Surveys.Views
{
    public class ChangePassword
    {
        public static void Change(Account account)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("New password: ");

            string password = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
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


            positionY++;           
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Repeat password: ");
            string repeat = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    repeat += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && repeat.Length > 0)
                    {
                        repeat = repeat.Substring(0, (repeat.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            Configuration.ConsoleClearToArtAscii();
            if (password == repeat)
            {
                UserSecurityRepository userSecurityRepository = new UserSecurityRepository();
                UserSecurity userSecurity = userSecurityRepository.GetSecurity(account.personData.personDataID);
                userSecurity.password = repeat;
                userSecurityRepository.EditUserSecurity(userSecurity);

                AfterSignIn.ComeBack(account, "Password was changed.");
            }
               
            AfterSignIn.ComeBack(account, "Passwords were different. Try again.");
        }
    }
}
