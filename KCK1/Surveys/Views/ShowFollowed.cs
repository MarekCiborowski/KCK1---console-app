﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace Surveys.Views
{
    public class ShowFollowed
    {
        public static void Show(Account account)
        {
            Configuration.SetConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepo = new AccountRepository();
            List<Account> followed = accountRepo.GetFollowedAccounts(account.accountID);

            Console.WriteLine("Quantity of Followers: " + followed.Count);
            positionY += 2;

            ConsoleKey key;
            int i = 0;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
            Configuration.CurrentConsoleLineClear(positionX);

            while(followed.Count > i)
            {
                if (i == 1) Console.ForegroundColor = Color.Red;
                else Console.ForegroundColor = Color.White;
                Console.Write("      " + followed[i].nickname + "      ");
                i++;
            }
            i = 1;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
            Configuration.CurrentConsoleLineClear(positionX);
            //Console.ForegroundColor = Color.DarkMagenta;
            //Console.Write(followed[1].GetDescription());
            //Console.ForegroundColor = Color.White;
            int quantityOfOptions = followed.Count;

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

                        for (int j = followed.Count-1; j >= 0; j--)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            int z = (i - j) % quantityOfOptions;
                            if (z == (-1))
                                Console.Write("      " + followed[quantityOfOptions - 1].nickname + "      ");
                            else if (z == (-2))
                            {
                                Console.Write("      " + followed[quantityOfOptions - 2].nickname + "      ");
                                i = quantityOfOptions;
                            }
                            else
                                Console.Write("      " + followed[z].nickname + "      ");
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
                        for (int j = 0; j < followed.Count; j++)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            Console.Write("      " + followed[(i + j) % quantityOfOptions].nickname + "      ");
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
                        PersonView.Show(account, followed[i]);
                        break;
                    case ConsoleKey.Escape:
                        Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
                        break;



                }

            }
        }
    }
}
