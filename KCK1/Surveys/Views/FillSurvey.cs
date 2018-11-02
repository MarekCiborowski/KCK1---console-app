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
    public class DisplayedAnswer
    {
        public Answer answer { get; set; }
        public bool isChecked { get; set; } = false;
        public int answerPositionY { get; set; }
    }
    public class FillSurvey
    {
        public static void Fill(Account account, Survey survey)
        {
            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();
            AnswerRepository answerRepository = new AnswerRepository();

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
            Console.WriteLine("Survey's author is " + surveyRepository.GetAuthor(survey.surveyID).nickname);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            if (survey.isAnonymous)
                Console.WriteLine("This survey is anonymous");
            else
                Console.WriteLine("This survey is not anonymous, however only author will see your answers");
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine("Do you want to start filling this survey? (y/n)");
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            string decision = Console.ReadLine();
            if(decision != "y")
                AfterSignIn.Start(account);

            

            Configuration.SetConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            positionX = 30; positionY = 15;
            Console.SetCursorPosition(positionX, positionY);



            List<Question> questions = surveyRepository.GetQuestions(survey.surveyID);
            int i = 1;
            foreach(Question question in questions)
            {
                
                Console.WriteLine("Question nr " + i + ": " + question.questionValue);
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Category currentQuestionCategory = questionRepository.GetQuestionCategory(question.questionID);
                bool canAddOwnAnswers = currentQuestionCategory.canAddOwnAnswer,
                    isSingleChoice = currentQuestionCategory.isSingleChoice;
                
                List<Answer> answers = questionRepository.GetAnswers(question.questionID);
                int firstAnswerPosition = positionY,answerNumber = 1;
                List<DisplayedAnswer> displayedAnswers = new List<DisplayedAnswer>();

                Console.ForegroundColor = Color.Red;
                foreach (Answer answer in answers)
                {
                    displayedAnswers.Add(new DisplayedAnswer
                    { answer = answer, answerPositionY = positionY });

                    Console.WriteLine(answerNumber +". " + answer.answerValue);
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
                    Console.ForegroundColor = Color.White;
                }
                if (canAddOwnAnswers)
                {
                    displayedAnswers.Add(new DisplayedAnswer
                    { answer = answerRepository.CreateAnswer("new answer"),
                        answerPositionY = positionY });

                    Console.WriteLine("Add your own answer");
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
                }
                int lastAnswerPosition = positionY - 1;
                ConsoleKey key;
                while (true)
                {
                    key = ConsoleKey.B;
                    if (Console.KeyAvailable)
                        key = Console.ReadKey(true).Key;
                }
                
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
