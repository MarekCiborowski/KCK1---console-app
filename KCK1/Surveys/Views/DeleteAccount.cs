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
    public class DeleteAccount
    {
        public static void Delete(Account account)
        {
            AccountRepository accountRepository = new AccountRepository();

            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            
            Console.WriteLine("Are you sure to delete account? All information about you will be deleted");
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
                            {
                                accountRepository.RemoveAccount(account.accountID);
                                Program.Start("Your account was deleted :(");
                            }   
                            else
                                AfterSignIn.ComeBack(account, "Returned to main menu.");
                            break;
                    }
                }
            }

        }
    }
}
