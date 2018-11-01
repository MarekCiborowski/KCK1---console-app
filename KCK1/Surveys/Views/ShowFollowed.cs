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
    public class ShowFollowed
    {
        public static void Show(Account account)
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            AccountRepository accountRepo = new AccountRepository();

            List<Account> followed = accountRepo.GetFollowedAccounts(account.accountID);
            //while (true)
            //{
            //    key = ConsoleKey.B;
            //    if (Console.KeyAvailable)
            //        key = Console.ReadKey(true).Key;
                foreach (Account a in followed)
            {
                Console.WriteLine(a.nickname);
            }
        }
    }
}
