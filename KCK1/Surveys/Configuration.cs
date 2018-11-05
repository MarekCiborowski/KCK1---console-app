using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;
using Surveys.Views;
using DatabaseLayer.Models;

namespace Surveys
{
    public class Configuration
    {
        public static void ChangeOption(bool isLeftPressed, int positionX, int positionY)
        {
            string[] desicion = { "      Yes      ", "      No      " };
            if (isLeftPressed)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = Color.White;
                Console.Write(desicion[0]);

                Console.ForegroundColor = Color.Red;
                Console.Write(desicion[1]);
            }
            else
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = Color.Red;
                Console.Write(desicion[0]);

                Console.ForegroundColor = Color.White;
                Console.Write(desicion[1]);
            }
        }

        public static void SetConsoleSize()
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

            SetConsoleSize();
        }

        //do czyszczenia linii
        public static void CurrentConsoleLineClear()
        {
            int currentline = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentline);
        }

        //do wyczyszczenia linii i powrotu na konkretne miejsce w osi X
        public static void CurrentConsoleLineClear(int currentXposition)
        {
            int currentline = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentXposition, currentline);
        }

        public static void ConsoleClearToArtAscii()
        {
            int currentline = Console.CursorTop;
            while(currentline > 8)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new String(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentline);
                currentline--;
            }
        }

        public static void MainMenu(List<MenuOptions> listOptions)
        {
            ConsoleClearToArtAscii();
            ConsoleKey key;
            int i;
            int positionX = 10;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
            CurrentConsoleLineClear(positionX);

            for (i = 0; i < 3; i++)
            {
                if (i == 1) Console.ForegroundColor = Color.Red;
                else Console.ForegroundColor = Color.White;
                Console.Write(listOptions[i].GetName());
            }
            i = 1;
            Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
            CurrentConsoleLineClear(positionX);
            int quantityOfOptions = listOptions.Count;

            while (true)
            {
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
                        CurrentConsoleLineClear(positionX);

                        for (int j = 2; j >= 0; j--)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            int z = (i - j) % quantityOfOptions;
                            if (z == (-1))
                                Console.Write(listOptions[quantityOfOptions - 1].GetName());
                            else if (z == (-2))
                            {
                                Console.Write(listOptions[quantityOfOptions - 2].GetName());
                                i = quantityOfOptions;
                            }
                            else
                                Console.Write(listOptions[z].GetName());
                        }
                        i = (i - 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        CurrentConsoleLineClear(positionX);

                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2);
                        CurrentConsoleLineClear(positionX);
                        for (int j = 0; j < 3; j++)
                        {
                            if (j == 1)
                                Console.ForegroundColor = Color.Red;
                            else
                                Console.ForegroundColor = Color.White;

                            Console.Write(listOptions[(i + j) % quantityOfOptions].GetName());
                        }
                        i = (i + 1) % quantityOfOptions;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        CurrentConsoleLineClear(positionX);

                        break;
                    case ConsoleKey.Enter:
                        ConsoleClearToArtAscii();
                        listOptions[i].OptionFunction();
                        break;
                    case ConsoleKey.Escape:
                        ConsoleClearToArtAscii();
                        MainMenu(Options.GetMainOptions());
                        break;
                }
            }
        }

        
    }
}
