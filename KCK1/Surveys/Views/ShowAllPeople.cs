using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;


namespace Surveys.Views
{
    public class ShowAllPeople
    {
        public static void Show(Account account)
        {
            Configuration.SetConsoleSize();
            Console.WriteLine(ArtAscii.GetMainTitleString());

            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepo = new AccountRepository();

            List<Account> accounts = accountRepo.GetAccountsToList();

            ConsoleKey key;
            int i = 0;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
            Configuration.CurrentConsoleLineClear(positionX);

            while(accounts.Count > i)
            {
                if (i == 1) Console.ForegroundColor = Color.Red;
                else Console.ForegroundColor = Color.White;
                Console.Write("      " + accounts[i].nickname + "      ");
                i++;
            }
            i = 1;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
            Configuration.CurrentConsoleLineClear(positionX);
            //Console.ForegroundColor = Color.DarkMagenta;
            //Console.Write(accounts[1].GetDescription());
            //Console.ForegroundColor = Color.White;
            int quantityOfOptions = accounts.Count;

            while (true)
            {
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
                        Configuration.CurrentConsoleLineClear(positionX);

                        for (int j = accounts.Count - 1; j >= 0; j--)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            int z = (i - j) % quantityOfOptions;
                            if (z == (-1))
                                Console.Write("      " + accounts[quantityOfOptions - 1].nickname + "      ");
                            else if (z == (-2))
                            {
                                Console.Write("      " + accounts[quantityOfOptions - 2].nickname + "      ");
                                i = quantityOfOptions;
                            }
                            else
                                Console.Write("      " + accounts[z].nickname + "      ");
                        }
                        i = (i - 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        Configuration.CurrentConsoleLineClear(positionX);
                        //Console.ForegroundColor = Color.DarkMagenta;
                        //Console.Write(accounts[i].GetDescription());
                        //Console.ForegroundColor = Color.White;

                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
                        Configuration.CurrentConsoleLineClear(positionX);
                        for (int j = 0; j < accounts.Count; j++)
                        {
                            if (i < 0)
                                i = -i;
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;
                            int value = (i + j) % quantityOfOptions;
                            Console.Write("      " + accounts[value].nickname + "      ");
                        }
                        i = (i + 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        Configuration.CurrentConsoleLineClear(positionX);
                        //Console.ForegroundColor = Color.DarkMagenta;
                        //Console.Write(accounts[i].GetDescription());
                        //Console.ForegroundColor = Color.White;

                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        PersonView.Show(account, accounts[i]);
                        break;
                    case ConsoleKey.Escape:
                        Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
                        break;



                }

            }
        }
    }
}
