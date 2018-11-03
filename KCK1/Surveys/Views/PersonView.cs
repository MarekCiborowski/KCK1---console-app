using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using DatabaseLayer.Models;
using DatabaseLayer;
using RepositoryLayer.Repositories;
using System.Drawing;

//Widok wykorzystywany po wejściu w podgląd profilu użytkownika w widoku ShowAllPeople

namespace Surveys.Views
{
    public class PersonView
    {
        public static void Show(Account account, Account accountToShow)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepository = new AccountRepository();

            
            Console.WriteLine("Nickname: " + accountToShow.nickname);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Email: " + accountToShow.email);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Country: " + accountToShow.personData.country);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Quantity of Followers: " + accountRepository.GetQuantityOfFollowersByID(accountToShow.accountID) );
            positionY += 2;
            Console.SetCursorPosition(positionX, positionY);

            if (account.accountID == accountToShow.accountID)
            {
                Console.WriteLine("It's your account! :D");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Press any button to continue");
                Console.ReadKey();
                Configuration.ConsoleClearToArtAscii();
                AfterSignIn.ComeBack(account, "You back to menu");
            }

            else if (!accountRepository.IsFollowed(account.accountID, accountToShow.accountID) )
            {
                Console.WriteLine("Do you want to follow " + accountToShow.nickname + "?");
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
                                    accountRepository.AddFollower(account.accountID, accountToShow.accountID);
                                    AfterSignIn.ComeBack(account, accountToShow.nickname + " was followed");
                                }                                
                                else
                                    AfterSignIn.ComeBack(account, "You back to menu");
                                break;
                        }
                    }
                }
            }                

            else
            {
                Console.WriteLine("Do you want to unfollow " + accountToShow.nickname + "?");
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
                                    accountRepository.RemoveFollower(account.accountID, accountToShow.accountID);
                                    AfterSignIn.ComeBack(account, accountToShow.nickname + " was unfollowed");
                                }
                                else
                                    AfterSignIn.ComeBack(account, "You back to menu");
                                break;
                        }
                    }
                }
            }             
        }
    }
}
