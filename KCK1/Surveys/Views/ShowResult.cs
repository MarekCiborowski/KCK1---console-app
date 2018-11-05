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


    public class ShowResult
    {
        private class DisplayedAnswer
        {
            public Answer answer { get; set; }

            public int answerPositionY { get; set; }
            public int answerNumber { get; set; }

        }
        private class DisplayedQuestion
        {
            public Question question { get; set; }
            public int questionPositionY { get; set; }
            public int questionNumber { get; set; }
        }

        public static void Show(Account account, Survey survey)
        {
            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();


            List<Question> surveyQuestions = surveyRepository.GetQuestions(survey.surveyID);
            List<DisplayedQuestion> displayedQuestions = new List<DisplayedQuestion>();
            int positionX = 30, positionY = 15;
            if (surveyRepository.GetQuantityOfVoters(survey.surveyID) == 0)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.WriteLine("No one has completed your survey yet. Press any key to return.");
                Console.ReadKey();
                Configuration.ConsoleClearToArtAscii();
                AfterSignIn.ComeBack(account, "");
            }
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.Azure;
            Console.WriteLine(survey.title);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.White;
            Console.WriteLine(survey.description);
            positionY++;

            int currentQuestionPosition = positionY, currentQuestionNumber = 1;
            foreach (Question question in surveyQuestions)
            {
                displayedQuestions.Add(new DisplayedQuestion
                {
                    question = question,
                    questionNumber = currentQuestionNumber,
                    questionPositionY = currentQuestionPosition
                });
                currentQuestionPosition++;
                currentQuestionNumber++;
            }
            ConsoleKey key;
            int currentlySelectedQuestion = 1, lastQuestionNumber = currentQuestionNumber - 1;

            while (true)
            {
                foreach (DisplayedQuestion displayedQuestion in displayedQuestions)
                {
                    Console.SetCursorPosition(positionX, displayedQuestion.questionPositionY);
                    if (currentlySelectedQuestion == displayedQuestion.questionNumber)
                        Console.ForegroundColor = Color.Red;
                    else
                        Console.ForegroundColor = Color.White;

                    Console.WriteLine(displayedQuestion.question.questionValue);


                }
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentlySelectedQuestion--;
                        if (currentlySelectedQuestion == 0)
                            currentlySelectedQuestion = lastQuestionNumber;
                        break;

                    case ConsoleKey.DownArrow:
                        currentlySelectedQuestion++;
                        if (currentlySelectedQuestion == lastQuestionNumber + 1)
                            currentlySelectedQuestion = 1;
                        break;

                    case ConsoleKey.Enter:
                        Configuration.ConsoleClearToArtAscii();
                        
                        Question question = displayedQuestions.Find(t => t.questionNumber == currentlySelectedQuestion).question;

                        ShowAnswers(question, survey.isAnonymous);
                        break;

                    case ConsoleKey.Escape:
                        Configuration.ConsoleClearToArtAscii();
                        AfterSignIn.ComeBack(account, "Returned to main menu");
                        break;
                }
            }
        }

        private static void ShowAnswers(Question question, bool isAnonymous)
        {

            QuestionRepository questionRepository = new QuestionRepository();
            AnswerRepository answerRepository = new AnswerRepository();

            List<Answer> questionAnswers = questionRepository.GetAnswers(question.questionID);
            List<DisplayedAnswer> displayedAnswers = new List<DisplayedAnswer>();
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.Azure;
            Console.WriteLine(question.questionValue);
            positionY++;

            string spaceBreak = new string(' ', 8);
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.White;
            Console.WriteLine("Answer" + spaceBreak + "Number of votes");
            positionY++;


            int currentAnswerPosition = positionY, currentAnswerNumber = 1;
            foreach (Answer answer in questionAnswers)
            {
                displayedAnswers.Add(new DisplayedAnswer
                {
                    answer = answer,
                    answerNumber = currentAnswerNumber,
                    answerPositionY = currentAnswerPosition
                });
                currentAnswerPosition++;
                currentAnswerNumber++;
            }
            ConsoleKey key;
            int currentlySelectedAnswer = 1, lastAnswerNumber = currentAnswerNumber - 1;
            if (isAnonymous)
            {
                foreach (DisplayedAnswer displayedAnswer in displayedAnswers)
                {
                    Console.SetCursorPosition(positionX, displayedAnswer.answerPositionY);
                    Console.ForegroundColor = Color.White;
                    string answerValue = displayedAnswer.answer.answerValue;
                    int customSpaceBreak = 30 - answerValue.Length;
                    Console.WriteLine(answerValue + new string(' ', customSpaceBreak) +
                        answerRepository.GetQuantityOfVotes(displayedAnswer.answer.answerID));
                }
                Console.SetCursorPosition(positionX, currentAnswerPosition + 2);
                Console.WriteLine("Press any key to go back to question selection screen.");
                Console.ReadKey();
                Configuration.ConsoleClearToArtAscii();
                return;
            }

            while (true)
            {
                foreach (DisplayedAnswer displayedAnswer in displayedAnswers)
                {
                    Console.SetCursorPosition(positionX, displayedAnswer.answerPositionY);
                    if (currentlySelectedAnswer == displayedAnswer.answerNumber)
                        Console.ForegroundColor = Color.Red;
                    else
                        Console.ForegroundColor = Color.White;

                    string answerValue = displayedAnswer.answer.answerValue;
                    int customSpaceBreak = 30 - answerValue.Length;
                    Console.WriteLine(answerValue + new string(' ', customSpaceBreak) +
                        answerRepository.GetQuantityOfVotes(displayedAnswer.answer.answerID));


                }
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentlySelectedAnswer--;
                        if (currentlySelectedAnswer == 0)
                            currentlySelectedAnswer = lastAnswerNumber;
                        break;

                    case ConsoleKey.DownArrow:
                        currentlySelectedAnswer++;
                        if (currentlySelectedAnswer == lastAnswerNumber + 1)
                            currentlySelectedAnswer = 1;
                        break;

                    case ConsoleKey.Enter:
                        Configuration.ConsoleClearToArtAscii();
                        //author
                        Answer answer = displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).answer;

                        ShowVoters(answer);
                        break;

                    case ConsoleKey.Escape:
                        Configuration.ConsoleClearToArtAscii();
                        return;

                }
            }

        }

        private static void ShowVoters(Answer answer)
        {
            AnswerRepository answerRepository = new AnswerRepository();

            List<Account> voters = answerRepository.GetAccountsVoters(answer.answerID);
            
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.WriteLine(answer.answerValue);
            positionY += 2;

            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = Color.Azure;
            Console.WriteLine("Nickname");
            positionY++;



            foreach (Account _account in voters)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = Color.White;
                Console.WriteLine(_account.nickname);
                positionY++;
            }
            Console.SetCursorPosition(positionX, positionY++);
            Console.WriteLine("Press any key to go back to answer selection screen.");
            Console.ReadKey();
            Configuration.ConsoleClearToArtAscii();
            return;

        }
    }
}
