using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Surveys.Views
{
    public class SignInView
    {
        public static void SignIn()
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());

            Console.SetCursorPosition(30, 23);
            Console.Write("Login: ");

            string login;
            login = Console.ReadLine();

            Console.SetCursorPosition(30, 24);
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

            Program.Start();

        }
    }
}
