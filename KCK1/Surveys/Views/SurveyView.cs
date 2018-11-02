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
            string desicion = "";
            if (account.accountID == author.accountID)
            {
                Console.WriteLine("You are an author of this survey. Do you want see results?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.Write("y/n: ");
                desicion = Console.ReadLine();
                if(desicion == "y")
                    ShowResult.Show(account, survey);
                AfterSignIn.ComeBack(account, "You back to menu");
            }
            Console.WriteLine("Author:          " + author.nickname);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            Console.WriteLine("Do you want to fill this survey?");
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("y/n: ");
            desicion = Console.ReadLine();
            if (desicion == "y")
                FillSurvey.Fill(account, survey);
            AfterSignIn.ComeBack(account, "You back to menu");


        }
    }
}
