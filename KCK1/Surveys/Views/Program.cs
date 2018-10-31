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
        public static void Start()
        {
            setConsoleSize();

            Console.WriteLine(getMainTitleString());
            Console.WriteLine("Press any button to continue");

            Console.ReadKey();
        }

        public static void setConsoleSize()
        {
            int heightOfWindow = 30;
            int widthOfWindow = 100;
            Console.SetWindowSize(widthOfWindow, heightOfWindow);
            Console.Clear();
        }

        public static string getMainTitleString()
        {
            string survey = @"
                    _________    ___    ____    ________    ___       __   _________    _________  
                   |\    ____\  |\  \  |\   \  |\   __  \  |\  \     |  | |\   _____\  |\______  \
                   \ \   \___|_ \ \  \  \ \  \ \ \  \|\  \ \ \  \    |  | \ \  \        \|_____\  \
                    \ \_____   \ \ \  \  \ \  \ \ \       \ \ \  \   |  |  \ \  \___            \  \
                     \|_____|\  \ \ \  \__\_\  \ \ \   _  _\ \ \  \  |  |   \ \   __\       __   \  \
                       _____|_\  \ \ \          \ \ \  \\  \  \ \  \ |  |    \ \  \______  |\ \___\  \
                      |\__________\ \_\__________\ \ \__\\ _\  \ \  \|  |     \ \________\ \ \________\
                      \|__________|  \|__________|  \|__|\|__|  \_\_____|      \|________|  \|________|";
            return survey;
        }
        
    }
}
