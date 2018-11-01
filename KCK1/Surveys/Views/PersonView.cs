using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using DatabaseLayer.Models;
using DatabaseLayer;
using RepositoryLayer.Repositories;

//Widok wykorzystywany po wejściu w podgląd profilu użytkownika w widoku ShowAllPeople

namespace Surveys.Views
{
    public class PersonView
    {
        public static void Show(Account account, Account accountToShow)
        {
            Configuration.SetConsoleSize();
            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepo = new AccountRepository();

            
            Console.WriteLine("Nickname: " + accountToShow.nickname);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Email: " + accountToShow.email);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Country: " + accountToShow.personData.country);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Quantity of Followers: " + accountRepo.GetQuantityOfFollowersByID(accountToShow.accountID) );
            positionY += 2;
            Console.SetCursorPosition(positionX, positionY);

            if (account.accountID == accountToShow.accountID)
            {
                Console.WriteLine("It's your account! :D");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Press any button to go back to list all users");
                Console.ReadKey();
                ShowAllPeople.Show(account);
            }

            else if (!accountRepo.IsFollowed(account.accountID, accountToShow.accountID) )
            {
                Console.Write("Do you want to follow " + accountToShow.nickname + "? Write y (yes) or n (no) ");
                string answer = "";
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    accountRepo.AddFollower(account.accountID, accountToShow.accountID);
                    AfterSignIn.ComeBack(account, accountToShow.nickname + " was followed");
                }
                else
                    ShowAllPeople.Show(account);
            }                

            else
            {
                Console.Write("Do you want to unfollow " + accountToShow.nickname + "? Write y (yes) or n (no) ");
                string answer = "";
                answer = Console.ReadLine();
                if (answer == "y")
                {
                    accountRepo.RemoveFollower(account.accountID, accountToShow.accountID);
                    AfterSignIn.ComeBack(account, accountToShow.nickname + " was unfollowed");
                }
                else
                    ShowAllPeople.Show(account);
            }
                

            


        }
    }
}
