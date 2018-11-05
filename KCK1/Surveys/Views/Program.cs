using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;

namespace Surveys.Views
{
    public class Program
    {
        static void Main(string[] args)
        {
            Configuration.ConsoleInitialize();
            Start();

        }

        public static void Start()
        {
            Configuration.SetConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            Console.SetCursorPosition(Console.WindowWidth / 3, 10);
            Console.WriteLine("Press any button to continue.");

            Console.ReadKey();

            Configuration.MainMenu(Options.GetMainOptions());
        }

        public static void ComeBack(string news)
        {
            Console.SetCursorPosition(Console.WindowWidth / 3, 10);
            Console.WriteLine(news);
            Console.SetCursorPosition(Console.WindowWidth / 3, 12);
            Console.WriteLine("Press any button to continue");

            Console.ReadKey();

            Configuration.MainMenu(Options.GetMainOptions());
        }
    }
}
