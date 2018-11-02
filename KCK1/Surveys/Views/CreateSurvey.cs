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

namespace Surveys.Views
{
    public class CreateSurvey
    {
        public static void Create(Account account)
        {
            Configuration.SetConsoleSize();
            Console.WriteLine(ArtAscii.GetMainTitleString());
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
            Console.Write("y/n: ");
            string isAnonymous = "";
            isAnonymous = Console.ReadLine();
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            bool anonymous = false;
            if (isAnonymous == "y")
                anonymous = true;
            
            ICollection<Question> questions = new List<Question>();
            
            do
            {
                Console.WriteLine("Can user add his/her own answer to question?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.Write("y/n: ");
                string canAddOwnAnswer = "";
                canAddOwnAnswer = Console.ReadLine();

                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("Is that question with a single choice?");
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Console.Write("y/n: ");
                string isSingleChoice = "";
                isSingleChoice = Console.ReadLine();

                bool addOwnAnswer = false, singleChoice = false;
                if (canAddOwnAnswer == "y")
                    addOwnAnswer = true;
                if (isSingleChoice == "y")
                    singleChoice = true;

                Category category = questionRepository.GetQuestionCategory(addOwnAnswer, singleChoice);

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
                        Console.Write("Stop adding answers? y/n: ");
                        string stop = "";
                        stop = Console.ReadLine();
                        positionY++;
                        Console.SetCursorPosition(positionX, positionY);
                        if (stop == "y")
                        {
                            positionY++;
                            Console.SetCursorPosition(positionX, positionY);
                            break;
                        }                           
                    }
                   
                    i++;
                } while (true);
                Question question = questionRepository.CreateQuestion(questionValue, addOwnAnswer, singleChoice, answers);
                questions.Add(question);
                Console.Write("Do you want to finish create survey? y/n: ");
                string finish = "";
                finish = Console.ReadLine();
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                if (finish == "y")
                    break;

            } while (true);

            Survey survey = surveyRepository.CreateSurvey(title, description, anonymous, questions);
            surveyRepository.AddSurvey(survey, account);

            AfterSignIn.ComeBack(account, "Survey was created!");
        }

    }
}
