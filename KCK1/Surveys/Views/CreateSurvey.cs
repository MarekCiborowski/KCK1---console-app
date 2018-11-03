using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;
using Surveys;
using System.Drawing;

namespace Surveys.Views
{
    public class CreateSurvey
    {
        public static void Create(Account account)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();
            AnswerRepository answerRepository = new AnswerRepository();

            Console.Write("Title: ");
            string title = "";
            title = Console.ReadLine();
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            Console.Write("Description: ");
            string description = "";
            description = Console.ReadLine();
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            Console.WriteLine("Anonymous Survey (As author you can see, who voted for the answer, show only nicknames)?");
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            //Console.Write("y/n: ");
            //string isAnonymous = "";
            //isAnonymous = Console.ReadLine();

            bool ifLeftPressed = true;
            Configuration.ChangeOption(false, positionX, positionY);
            bool exitWhile = true;
            bool anonymous = false;
            while (exitWhile)
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
                        case ConsoleKey.Enter:
                            Console.ForegroundColor = Color.White;
                            exitWhile = false;
                            if (ifLeftPressed)
                                anonymous = true;
                            break;
                    }
                }
            }

            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            
            ICollection<Question> questions = new List<Question>();
            bool addOwnAnswer = false, singleChoice = false;
            bool anotherExit = true;
            do
            {
                Console.WriteLine("Can user add his/her own answer to question?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);

                ifLeftPressed = true;
                Configuration.ChangeOption(false, positionX, positionY);
                exitWhile = true;

                while (exitWhile)
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
                            case ConsoleKey.Enter:
                                Console.ForegroundColor = Color.White;
                                exitWhile = false;
                                if (ifLeftPressed)
                                    addOwnAnswer = true;
                                break;
                        }
                    }
                }
                exitWhile = true;

                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Is that question with a single choice?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);

                ifLeftPressed = true;
                Configuration.ChangeOption(false, positionX, positionY);
                exitWhile = true;

                while (exitWhile)
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
                            case ConsoleKey.Enter:
                                Console.ForegroundColor = Color.White;
                                exitWhile = false;
                                if (ifLeftPressed)
                                    singleChoice = true;
                                break;
                        }
                    }
                }

                int categoryID = questionRepository.GetQuestionCategory(addOwnAnswer, singleChoice);

                positionY += 2;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Ask for something.");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                string questionValue = "";
                questionValue = Console.ReadLine();
                positionY += 2;
                Console.SetCursorPosition(positionX, positionY);
                int i = 1;
                ICollection<Answer> answers = new List<Answer>();
                
                do
                {
                    Console.Write("Answer nr " + i + ": ");
                    string ans = "";
                    ans = Console.ReadLine();
                    Answer answer = answerRepository.CreateAnswer(ans);
                    answers.Add(answer);
                    positionY++;
                    Console.SetCursorPosition(positionX, positionY);
                    if (i >= 2)
                    {
                        Console.WriteLine("Stop adding answers?");
                        positionY++;
                        Console.SetCursorPosition(positionX, positionY);
                        ifLeftPressed = true;
                        Configuration.ChangeOption(false, positionX, positionY);
                        exitWhile = true;

                        while (exitWhile)
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
                                    case ConsoleKey.Enter:
                                        Console.ForegroundColor = Color.White;
                                        exitWhile = false;
                                        if (ifLeftPressed)
                                            anotherExit = false;
                                        else
                                        {
                                            positionY++;
                                            Console.SetCursorPosition(positionX, positionY);
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    i++;
                } while (anotherExit);
                anotherExit = true;
                Question question = questionRepository.CreateQuestion(questionValue, addOwnAnswer, singleChoice, answers);
                questions.Add(question);
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Do you want to finish create survey?");

                positionY++;
                Console.SetCursorPosition(positionX, positionY);

                ifLeftPressed = true;
                Configuration.ChangeOption(false, positionX, positionY);
                exitWhile = true;
                while (exitWhile)
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
                            case ConsoleKey.Enter:
                                Console.ForegroundColor = Color.White;
                                exitWhile = false;
                                if (ifLeftPressed)
                                    anotherExit = false;
                                else
                                {
                                    positionY++;
                                    Console.SetCursorPosition(positionX, positionY);
                                }
                                break;
                        }
                    }
                }
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
            } while (anotherExit);

            Configuration.ConsoleClearToArtAscii();
            Survey survey = surveyRepository.CreateSurvey(title, description, anonymous, questions);
            surveyRepository.AddSurvey(survey, account);

            AfterSignIn.ComeBack(account, "Survey was created!");
        }

    }
}
