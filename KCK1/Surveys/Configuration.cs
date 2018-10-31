﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

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

        public static void MainMenu(List<MenuOptions> listOptions)
        {
            setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());

            ConsoleKey key;
            int i;
            int positionX = 30;
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
            Console.ForegroundColor = Color.DarkMagenta;
            Console.Write(listOptions[1].GetDescription());
            Console.ForegroundColor = Color.White;
            int liczbaOpcji = listOptions.Count;

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

                            int z = (i - j) % liczbaOpcji;
                            if (z == (-1))
                                Console.Write(listOptions[liczbaOpcji - 1].GetName());
                            else if (z == (-2))
                            {
                                Console.Write(listOptions[liczbaOpcji - 2].GetName());
                                i = liczbaOpcji;
                            }
                            else
                                Console.Write(listOptions[z].GetName());
                        }
                        i = (i - 1) % liczbaOpcji;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        CurrentConsoleLineClear(positionX);
                        Console.ForegroundColor = Color.DarkMagenta;
                        Console.Write(listOptions[i].GetDescription());
                        Console.ForegroundColor = Color.White;

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

                            Console.Write(listOptions[(i + j) % liczbaOpcji].GetName());
                        }
                        i = (i + 1) % liczbaOpcji;

                        Console.SetCursorPosition(positionX, Console.WindowHeight / 2 + 5);
                        CurrentConsoleLineClear(positionX);
                        Console.ForegroundColor = Color.DarkMagenta;
                        Console.Write(listOptions[i].GetDescription());
                        Console.ForegroundColor = Color.White;

                        break;
                    case ConsoleKey.Enter:
                        listOptions[i].OptionFunction();
                        break;
                    case ConsoleKey.Escape:
                        MainMenu(Options.GetOptions());
                        break;
                }
            }
        }

    }
}