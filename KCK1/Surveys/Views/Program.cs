using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using System.Drawing;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

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
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.getMainTitleString());
            Console.SetCursorPosition(Console.WindowWidth / 3, 10);
            Console.WriteLine("Press any button to continue");

            Console.ReadKey();

            SignInView.Menu();
        }    
    }
}
