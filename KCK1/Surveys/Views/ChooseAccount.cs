using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace Surveys.Views
{
    public class DisplayedAccount
    {
        public Account account { get; set; }
        public int accountPositionY { get; set; }
        public int accountNumber { get; set; }
        public int numberOfFollowers { get; set; }
        public int numberOfSurveysCreated { get; set; }

    }
    public class ChooseAccount
    {
        public static void Choose(Account account, List<Account> accounts)
        {
            AccountRepository accountRepository = new AccountRepository();

            Configuration.ConsoleClearToArtAscii();
            int positionX = 30;
            if (!accounts.Any())
            {
                AfterSignIn.ComeBack(account, "There are no accounts to display in this category");
            }


            List<DisplayedAccount> displayedAccounts = new List<DisplayedAccount>();
            int currentAccountPosition = 15, currentAccountNumber = 1;

            Console.SetCursorPosition(positionX, currentAccountPosition);
            Console.ForegroundColor = Color.White;
            string spaceBreak = new string(' ', 6);
            string bigSpaceBreak = new string(' ', 18);

            Console.WriteLine("Nickname" + spaceBreak + "Number of followers" + spaceBreak + "Number of created surveys");
            currentAccountPosition+=2;

            foreach (Account _account in accounts)
            {
                displayedAccounts.Add(new DisplayedAccount
                {
                    account = _account,
                    accountNumber = currentAccountNumber,
                    accountPositionY = currentAccountPosition,
                    numberOfFollowers = accountRepository.GetQuantityOfFollowersByID(_account.accountID),
                    numberOfSurveysCreated = accountRepository.GetAccountAuthorSurveys(_account.accountID).Count  
                });
                currentAccountPosition++;
                currentAccountNumber++;
            }
            ConsoleKey key;
            int currentlySelectedAccount = 1, lastAccountNumber = currentAccountNumber-1;
            while (true)
            {
                foreach (DisplayedAccount displayedAccount in displayedAccounts)
                {
                    Console.SetCursorPosition(positionX, displayedAccount.accountPositionY);
                    if (currentlySelectedAccount == displayedAccount.accountNumber)
                        Console.ForegroundColor = Color.Red;
                    else
                        Console.ForegroundColor = Color.White;
                    int customSpaceBreakLength = 24 - displayedAccount.account.nickname.Length;
                    Console.WriteLine(displayedAccount.account.nickname + new string(' ', customSpaceBreakLength) + displayedAccount.numberOfFollowers +
                        bigSpaceBreak + spaceBreak + displayedAccount.numberOfSurveysCreated);


                }
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentlySelectedAccount--;
                        if (currentlySelectedAccount == 0)
                            currentlySelectedAccount = lastAccountNumber;
                        break;

                    case ConsoleKey.DownArrow:
                        currentlySelectedAccount++;
                        if (currentlySelectedAccount == lastAccountNumber+1)
                            currentlySelectedAccount = 1;
                        break;

                    case ConsoleKey.Enter:
                        Configuration.ConsoleClearToArtAscii();
                        PersonView.Show(account, displayedAccounts.Find(t => t.accountNumber == currentlySelectedAccount).account);
                        break;

                    case ConsoleKey.Escape:
                        Configuration.ConsoleClearToArtAscii();
                        AfterSignIn.ComeBack(account, "Returned to main menu.");
                        break;
                }
            }
        }


    }
}
