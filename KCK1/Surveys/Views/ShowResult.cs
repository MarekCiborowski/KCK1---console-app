using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;
using System.Drawing;

namespace Surveys.Views
{
    public class ShowResult
    {
        public static void Show(Account account, Survey survey)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            Console.Write("Show Result, press any button to do something");

            Console.ReadKey();

            Configuration.ConsoleClearToArtAscii();
            AfterSignIn.ComeBack(account, "You back from Result.");
        }
    }
}
