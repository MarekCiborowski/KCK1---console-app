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
    public class SurveyView
    {
        public static void Show(Account account, Survey survey)
        {
            Configuration.SetConsoleSize();
            Console.WriteLine(ArtAscii.GetMainTitleString());
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            SurveyRepository surveyRepository = new SurveyRepository();

            Account author = surveyRepository.GetAuthor(survey.surveyID);

            Console.WriteLine("Title:           " + survey.title);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Description:     " + survey.description);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            bool ifLeftPressed = true;
            if (account.accountID == author.accountID)
            {
                Console.WriteLine("You are an author of this survey. Do you want see results?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Configuration.ChangeOption(false, positionX, positionY);           
                while (true)
                {
                    ConsoleKey choice;
                    if (Console.KeyAvailable)
                    {
                        choice = Console.ReadKey(true).Key;
                        switch (choice)
                        {
                            case ConsoleKey.LeftArrow:
                                if (!ifLeftPressed)
                                {
                                    Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                    ifLeftPressed = !ifLeftPressed;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (ifLeftPressed)
                                {
                                    Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                    ifLeftPressed = !ifLeftPressed;
                                }
                                break;
                            case ConsoleKey.Escape:
                                AfterSignIn.ComeBack(account, "You back to menu");
                                break;
                            case ConsoleKey.Enter:
                                Console.ForegroundColor = Color.White;
                                if (ifLeftPressed)
                                    ShowResult.Show(account, survey);
                                else
                                    AfterSignIn.ComeBack(account, "You back to menu");
                                break;
                        }
                    }
                }
            }
            Console.WriteLine("Author:          " + author.nickname);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            Console.WriteLine("Do you want to fill this survey?");
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            Configuration.ChangeOption(false, positionX, positionY);
            while (true)
            {
                ConsoleKey choice;
                if (Console.KeyAvailable)
                {
                    choice = Console.ReadKey(true).Key;
                    switch (choice)
                    {
                        case ConsoleKey.LeftArrow:
                            if (!ifLeftPressed)
                            {
                                Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                ifLeftPressed = !ifLeftPressed;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (ifLeftPressed)
                            {
                                Configuration.ChangeOption(ifLeftPressed, positionX, positionY);
                                ifLeftPressed = !ifLeftPressed;
                            }
                            break;
                        case ConsoleKey.Escape:
                            AfterSignIn.ComeBack(account, "You back to menu");
                            break;
                        case ConsoleKey.Enter:
                            Console.ForegroundColor = Color.White;
                            if (ifLeftPressed)
                                FillSurvey.Fill(account, survey);
                            else
                                AfterSignIn.ComeBack(account, "You back to menu");
                            break;
                    }
                }
            }
        }
    }
}
