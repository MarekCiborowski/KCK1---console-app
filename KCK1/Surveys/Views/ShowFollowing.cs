using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using System.Drawing;

namespace Surveys.Views
{
    public class ShowFollowing
    {
        public static void Show(Account account)
        {
            Configuration.SetConsoleSize();
            Console.ForegroundColor = Color.White;
            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepository = new AccountRepository();
            List<Account> following = accountRepository.GetFollowingAccounts(account.accountID);

            if (following.Count == 0)
            {
                Console.WriteLine("Nobody is following you. Press any button to continue");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.ReadKey();
                AfterSignIn.ComeBack(account, "You back to menu");
            }

            Console.WriteLine("Quantity of Followers: " + following.Count);
            positionY += 2;

            ConsoleKey key;
            int i = 0;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
            Configuration.CurrentConsoleLineClear(positionX);

            while (following.Count > i)
            {
                if(following.Count == 1)
                    Console.ForegroundColor = Color.Red;
                else if (i == 1) Console.ForegroundColor = Color.Red;
                else Console.ForegroundColor = Color.White;
                Console.Write("      " + following[i].nickname + "      ");
                i++;
            }
            i = 1;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
            Configuration.CurrentConsoleLineClear(positionX);
            //Console.ForegroundColor = Color.DarkMagenta;
            //Console.Write(followed[1].GetDescription());
            //Console.ForegroundColor = Color.White;
            int quantityOfOptions = following.Count;

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

                        for (int j = following.Count - 1; j >= 0; j--)
                        {
                            if(following.Count == 1)
                                Console.ForegroundColor = Color.Red;
                            else if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            int z = (i - j) % quantityOfOptions;
                            if (z == (-1))
                                Console.Write("      " + following[quantityOfOptions - 1].nickname + "      ");
                            else if (z == (-2))
                            {
                                Console.Write("      " + following[quantityOfOptions - 2].nickname + "      ");
                                i = quantityOfOptions;
                            }
                            else
                                Console.Write("      " + following[z].nickname + "      ");
                        }
                        if(quantityOfOptions != 0)
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
                        for (int j = 0; j < following.Count; j++)
                        {
                            if(following.Count == 1)
                                Console.ForegroundColor = Color.Red;
                            else if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            Console.Write("      " + following[(i + j) % quantityOfOptions].nickname + "      ");
                        }
                        if(quantityOfOptions != 0)
                            i = (i + 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        Configuration.CurrentConsoleLineClear(positionX);
                        //Console.ForegroundColor = Color.DarkMagenta;
                        //Console.Write(accounts[i].GetDescription());
                        //Console.ForegroundColor = Color.White;

                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.ForegroundColor = Color.White;
                        PersonView.Show(account, following[i]);
                        break;
                    case ConsoleKey.Escape:
                        Console.ForegroundColor = Color.White;
                        Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
                        break;
                }
            }
        }
    }
}
