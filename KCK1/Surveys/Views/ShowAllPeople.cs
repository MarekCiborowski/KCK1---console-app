using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

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
            //while (true)
            //{
            //    key = ConsoleKey.B;
            //    if (Console.KeyAvailable)
            //        key = Console.ReadKey(true).Key;
            foreach (Account a in accounts)
            {
                Console.WriteLine(a.nickname + " " + a.personData.country);
                Console.SetCursorPosition(positionX, positionY);
                positionY++;
            }
        }
       
    }
}
