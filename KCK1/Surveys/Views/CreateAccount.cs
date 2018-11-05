using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using RepositoryLayer.Repositories;
using DatabaseLayer.Models;
using System.Drawing;

namespace Surveys.Views
{
    public class CreateAccount
    {
        public static void Create()
        {
            AccountRepository accountRepository = new AccountRepository();
            UserSecurityRepository userSecurityRepository = new UserSecurityRepository();
            PersonDataRepository personDataRepository = new PersonDataRepository();
            int positionX = 30, positionY = 15;
            ConsoleKeyInfo key;
            Console.ForegroundColor = Color.White;

            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Login: ");
            string login = "";
            login = Console.ReadLine();
            if(!userSecurityRepository.IsLoginFree(login))
                while(!userSecurityRepository.IsLoginFree(login))
                {
                    Configuration.ConsoleClearToArtAscii();
                    positionX = 30; positionY = 15;
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("This login is busy. Please try another: ");
                    login = Console.ReadLine();
                }
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            
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
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Repeat password: ");
            string repeat = "";
            do
            {
                key = Console.ReadKey(true);
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
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            if(password != repeat)
            {
                while(password != repeat)
                {
                    Configuration.ConsoleClearToArtAscii();
                    positionY = 15;
                    Console.SetCursorPosition(positionX, positionY);
                    Console.Write("Passwords were different. Try again");
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
                    Console.Write("Password: ");
                    password = "";
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
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
      
                    Console.Write("Repeat password: ");
                    repeat = "";
                    do
                    {
                        key = Console.ReadKey(true);
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
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
                }
                
            }
            positionY++;
            Console.Write("Nickname: ");
            string nickname = "";
            nickname = Console.ReadLine();
            if (!accountRepository.IsNicknameCorrect(nickname))
                while (!accountRepository.IsNicknameCorrect(nickname))
                {
                    Configuration.ConsoleClearToArtAscii();
                    positionX = 30; positionY = 15;
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("This nickname is busy or not correct.");
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("Length of nickname is 3-10 characters. Please try another: ");
                    nickname = Console.ReadLine();
                }
            
            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.Write("Email: ");
            string email = "";
            email = Console.ReadLine();
            if (!accountRepository.IsEmailCorrect(email))
                while (!accountRepository.IsEmailCorrect(email))
                {
                    Configuration.ConsoleClearToArtAscii();
                    positionX = 30; positionY = 15;
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("This email is busy or not correct.");
                    Console.SetCursorPosition(positionX, positionY);
                    positionY++;
                    Console.Write("Correct format: abcd@abcd.com. Please try another: ");
                    email = Console.ReadLine();
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

            PersonData personData = personDataRepository.CreatePersonData(address, city, zipcode, state, country);              
            UserSecurity userSecurity = userSecurityRepository.CreateUserSecurity(login, password);                      
            Account account = accountRepository.CreateAccount(personData, email, nickname, userSecurity);
            accountRepository.AddAccount(account);

            Configuration.ConsoleClearToArtAscii();
            Program.Start("Account was created.");
        }


    }
}
