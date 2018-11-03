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
    public class ShowMySurveys
    {
        public static void Show(Account account)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 23, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepository = new AccountRepository();

            List<Survey> surveys = accountRepository.GetAccountAuthorSurveys(account.accountID);

            if(surveys.Count == 0)
            {
                Console.WriteLine("You don't have any surveys. Do you want to create your first survey?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                bool ifLeftPressed = true;
                Configuration.ChangeOption(false, positionX, positionY);
                bool exitWhile = true;
                while (exitWhile)
                {
                    ConsoleKey choice;
                    if (Console.KeyAvailable)
                    {
                        choice = Console.ReadKey(true).Key;
                        switch (choice)
                        {
                            case ConsoleKey.LeftArrow:
                                if (!ifLeftPressed)
                                {
                                    Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                    ifLeftPressed = !ifLeftPressed;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (ifLeftPressed)
                                {
                                    Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                    ifLeftPressed = !ifLeftPressed;
                                }
                                break;
                            case ConsoleKey.Enter:
                                Console.ForegroundColor = Color.White;
                                exitWhile = false;
                                Configuration.ConsoleClearToArtAscii();
                                if (ifLeftPressed)
                                    CreateSurvey.Create(account);
                                AfterSignIn.ComeBack(account, "You back to menu");
                                break;
                        }
                    }
                }

            }

            ConsoleKey key;
            int i = 0;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
            Configuration.CurrentConsoleLineClear(positionX);

            while (3 > i)
            {
                if (i == 1) Console.ForegroundColor = Color.Red;
                else Console.ForegroundColor = Color.White;
                Console.Write("      " + surveys[i].title + "      ");
                i++;
            }
            i = 1;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
            Configuration.CurrentConsoleLineClear(positionX);
            //Console.ForegroundColor = Color.DarkMagenta;
           // Console.Write(surveys[1].description);
           // Console.ForegroundColor = Color.White;
            int quantityOfOptions = surveys.Count;

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

                        for (int j = 2; j >= 0; j--)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            int z = (i - j) % quantityOfOptions;
                            if (z == (-1))
                                Console.Write("      " + surveys[quantityOfOptions - 1].title + "      ");
                            else if (z == (-2))
                            {
                                Console.Write("      " + surveys[quantityOfOptions - 2].title + "      ");
                                i = quantityOfOptions;
                            }
                            else
                                Console.Write("      " + surveys[z].title + "      ");
                        }
                        if(quantityOfOptions != 0)
                            i = (i - 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        Configuration.CurrentConsoleLineClear(positionX);
                       // Console.ForegroundColor = Color.DarkMagenta;
                       // Console.Write(surveys[i].description);
                       // Console.ForegroundColor = Color.White;

                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
                        Configuration.CurrentConsoleLineClear(positionX);
                        for (int j = 0; j < 3; j++)
                        {
                            if (i < 0)
                                i = -i;
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;
                            int value = (i + j) % quantityOfOptions;
                            Console.Write("      " + surveys[value].title + "      ");
                        }
                        if (quantityOfOptions != 0)
                            i = (i + 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        Configuration.CurrentConsoleLineClear(positionX);
                       // Console.ForegroundColor = Color.DarkMagenta;
                       // Console.Write(surveys[i].description);
                       // Console.ForegroundColor = Color.White;

                        break;
                    case ConsoleKey.Enter:
                        Configuration.ConsoleClearToArtAscii();
                        Console.ForegroundColor = Color.White;
                        SurveyView.Show(account, surveys[i]);
                        break;
                    case ConsoleKey.Escape:
                        Configuration.ConsoleClearToArtAscii();
                        Console.ForegroundColor = Color.White;
                        Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
                        break;



                }

            }

        }
    }
}
