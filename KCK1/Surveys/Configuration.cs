using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys
{
    public class Configuration
    {

        public static void setConsoleSize()
        {
            int heightOfWindow = 30;
            int widthOfWindow = 130;
            Console.SetWindowSize(widthOfWindow, heightOfWindow);
            Console.Clear();
        }

        public static void ConsoleInitialize()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.Title = "Survey";

            setConsoleSize();
        }
    }
}
