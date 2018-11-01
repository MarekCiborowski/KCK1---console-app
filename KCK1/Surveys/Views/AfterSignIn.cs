using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace Surveys.Views
{
    public class AfterSignIn
    {
        public static void Start(Account account)
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Welcome in Survey Program! Press any button to continue.");

            Console.ReadKey();

            Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
        }

        public static void ComeBack(Account account, string news)
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            positionY++;
            Console.WriteLine(news);
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Press any button to continue.");

            Console.ReadKey();

            Configuration.MainMenu(Options.GetOptionsAfterSignIn(account));
        }
    }
}
