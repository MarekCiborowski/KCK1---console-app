using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;


namespace Surveys.Views
{
    public class FillSurvey
    {
        public static void Fill(Account account, Survey survey)
        {
            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();

            Configuration.SetConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.SkyBlue;
            Console.WriteLine(survey.title);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine(survey.description);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            //Console.WriteLine
            List<Question> questions = surveyRepository.GetQuestions(survey.surveyID);

            foreach(Question question in questions)
            {
                Category currentQuestionCategory = questionRepository.GetQuestionCategory(question.questionID);
                Console.WriteLine(question.questionValue);
                
            }

            //Console.Write("Login: ");
            //string login;
            //login = Console.ReadLine();

            //positionY++;
            //Console.SetCursorPosition(positionX, positionY);
            //Console.Write("Password: ");
            //string password = "";
            //do
            //{
            //    ConsoleKeyInfo key = Console.ReadKey(true);
            //    // Backspace Should Not Work
            //    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            //    {
            //        password += key.KeyChar;
            //        Console.Write("*");
            //    }
            //    else
            //    {
            //        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            //        {
            //            password = password.Substring(0, (password.Length - 1));
            //            Console.Write("\b \b");
            //        }
            //        else if (key.Key == ConsoleKey.Enter)
            //        {
            //            break;
            //        }
            //    }
            //} while (true);

            //AccountRepository accountRepository = new AccountRepository();
            //Account account = null;
            //account = accountRepository.GetAccount(login, password);
            //if (account == null)
            //    Program.Start("Wrong login or password, try again.");

            //AfterSignIn.Start(account);

        }

    }
}
