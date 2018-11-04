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
        public bool isSelected { get; set; } = false;
        public int answerPositionY { get; set; }
        public int answerNumber { get; set; }
        public bool addingOwnQuestion { get; set; } = false;
    }
    public class FillSurvey
    {
        public static void Fill(Account account, Survey survey)
        {
            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();
            AnswerRepository answerRepository = new AnswerRepository();
            AccountSurveyRepository accountSurveyRepository = new AccountSurveyRepository();

          //  Configuration.SetConsoleSize();

          //  Console.WriteLine(ArtAscii.GetMainTitleString());
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

            bool ifLeftPressed = true;
            Configuration.ChangeOption(false, positionX, positionY);
            bool exitWhile = true;
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
                        case ConsoleKey.Escape:
                            Configuration.ConsoleClearToArtAscii();
                            AfterSignIn.ComeBack(account, "You back to menu");
                            break;
                        case ConsoleKey.Enter:
                            Console.ForegroundColor = Color.White;
                            if (ifLeftPressed)
                                exitWhile = false;
                            else
                            {
                                Configuration.ConsoleClearToArtAscii();
                                AfterSignIn.ComeBack(account, "You back to menu");
                            }
                                
                            break;
                    }
                }
            }
            Configuration.ConsoleClearToArtAscii();
            positionX = 30; positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            List<Question> questions = surveyRepository.GetQuestions(survey.surveyID);
            int i = 1;
            foreach(Question question in questions)
            {
                positionY = 15;
                Configuration.ConsoleClearToArtAscii();
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = Color.White;
                Console.WriteLine("Question nr " + i + ": " + question.questionValue);
                i++;
                positionY++;
                Console.SetCursorPosition(positionX, positionY);
                Category currentQuestionCategory = questionRepository.GetQuestionCategory(question.questionID);
                bool canAddOwnAnswers = currentQuestionCategory.canAddOwnAnswer,
                    isSingleChoice = currentQuestionCategory.isSingleChoice;
                
                List<Answer> answers = questionRepository.GetAnswers(question.questionID);
                int answerPosition = positionY,answerNumber = 1;
                List<DisplayedAnswer> displayedAnswers = new List<DisplayedAnswer>();

                
                foreach (Answer answer in answers)
                {
                    displayedAnswers.Add(new DisplayedAnswer
                    { answer = answer, answerPositionY = answerPosition,answerNumber=answerNumber  });
                    answerPosition++;
                    answerNumber++;
                    
                }
                if (canAddOwnAnswers)
                {
                    displayedAnswers.Add(new DisplayedAnswer
                    { answer = answerRepository.CreateAnswer("Add your own answer."),
                        answerPositionY = answerPosition, answerNumber = answerNumber, addingOwnQuestion = true });
                    answerPosition++;
                    answerNumber++;

                }
                int confirmAnswersNumber = answerNumber, confirmAnswersPosition = answerPosition, currentlySelectedAnswer = 1;
                ConsoleKey key;
                bool isQuestionCompleted = false;
                displayedAnswers.Find(p => p.answerNumber == 1).isSelected = true;
                while (true)
                {
                    if (isQuestionCompleted)
                        break;

                    foreach(DisplayedAnswer displayedAnswer in displayedAnswers)
                    {
                        Console.SetCursorPosition(positionX, displayedAnswer.answerPositionY);
                        if (displayedAnswer.isChecked && displayedAnswer.isSelected)
                            Console.ForegroundColor = Color.Purple;
                        else if (displayedAnswer.isChecked && !displayedAnswer.isSelected)
                            Console.ForegroundColor = Color.Blue;
                        else if (!displayedAnswer.isChecked && displayedAnswer.isSelected)
                            Console.ForegroundColor = Color.Red;
                        else
                            Console.ForegroundColor = Color.White;

                        Console.WriteLine(displayedAnswer.answerNumber + ". " + displayedAnswer.answer.answerValue);
                    }
                    Console.SetCursorPosition(positionX, confirmAnswersPosition);
                    if (currentlySelectedAnswer == confirmAnswersNumber)
                        Console.ForegroundColor = Color.Red;
                    else
                        Console.ForegroundColor = Color.White;
                    Console.WriteLine("Confirm answers");
                    key = ConsoleKey.B;
                    if (Console.KeyAvailable)
                        key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if(currentlySelectedAnswer!=confirmAnswersNumber)
                                displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isSelected = false;
                            currentlySelectedAnswer--;

                            if (currentlySelectedAnswer == 0)
                                currentlySelectedAnswer = confirmAnswersNumber;
                            else
                            {
                                displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isSelected = true;
                            }

                            break;

                        case ConsoleKey.DownArrow:
                            if (currentlySelectedAnswer != confirmAnswersNumber)
                                displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isSelected = false;
                            currentlySelectedAnswer++;

                            if (currentlySelectedAnswer == confirmAnswersNumber)
                                break;

                            else if(currentlySelectedAnswer==confirmAnswersNumber + 1)
                                currentlySelectedAnswer = 1;
                            
                            displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isSelected = true;

                            break;

                        case ConsoleKey.Enter:

                            Console.SetCursorPosition(positionX, confirmAnswersPosition + 2);
                            Console.Write(new string(' ', Console.WindowWidth));

                            // Adding answer was selected
                            if (currentlySelectedAnswer != confirmAnswersNumber && displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).addingOwnQuestion)
                            {
                                displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).addingOwnQuestion = false;
                                Console.SetCursorPosition(positionX, displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).answerPositionY);
                                Console.Write(new string(' ', Console.WindowWidth));
                                string myAnswer = string.Empty;
                                while (string.IsNullOrEmpty(myAnswer))
                                {
                                    Console.SetCursorPosition(positionX, displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).answerPositionY);
                                    myAnswer = Console.ReadLine();
                                }
                                Answer _myAnswer = answerRepository.CreateAnswer(myAnswer);
                                answerRepository.AddAnswerToQuestion(_myAnswer, question.questionID);
                                displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).answer = _myAnswer;
                                break;
                            }

                            // Single choice question
                            if (currentQuestionCategory.isSingleChoice)
                            {
                                if (currentlySelectedAnswer == confirmAnswersNumber)
                                {
                                    //Checking if any answer was checked
                                    if (!displayedAnswers.Any(t => t.isChecked))
                                    {
                                        Console.SetCursorPosition(positionX, confirmAnswersPosition + 2);
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("To proceed you have to select at least one answer");
                                        break;
                                    }
                                    else
                                    {
                                        DisplayedAnswer checkedAnswer = displayedAnswers.Find(t => t.isChecked);
                                        answerRepository.AddVoteToAnswer(account.accountID, checkedAnswer.answer.answerID);
                                        isQuestionCompleted = true;
                                    }
                                }
                                else
                                {
                                    foreach(DisplayedAnswer displayedAnswer in displayedAnswers)
                                    {
                                        if(displayedAnswer.isChecked && displayedAnswer.answerNumber != currentlySelectedAnswer)
                                        {
                                            displayedAnswer.isChecked = false;
                                            break;
                                        }
                                    }

                                    displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isChecked =
                                        !displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isChecked;
                                    //wiem, słabe



                                }

                            }


                            // Multiple choice question
                            else
                            {
                                if (currentlySelectedAnswer == confirmAnswersNumber)
                                {
                                    //Checking if any answer was checked
                                    if (!displayedAnswers.Any(t => t.isChecked))
                                    {
                                        Console.SetCursorPosition(positionX, confirmAnswersNumber + 2);
                                        Console.ForegroundColor = Color.White;
                                        Console.WriteLine("To proceed you have to select at least one answer");
                                        break;
                                    }
                                    else
                                    {
                                        foreach (DisplayedAnswer checkedAnswer in displayedAnswers.Where(t => t.isChecked))
                                        {
                                            answerRepository.AddVoteToAnswer(account.accountID, checkedAnswer.answer.answerID);
                                        }
                                        isQuestionCompleted = true;
                                    }
                                }
                                else
                                {
                                    displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isChecked =
                                        !displayedAnswers.Find(t => t.answerNumber == currentlySelectedAnswer).isChecked;
                                }
                            }

                            

                            break;


                    }
                }
                
            }
            accountSurveyRepository.AddAccountSurvey(account.accountID, survey.surveyID);

            //ankieta skonczona

            Configuration.ConsoleClearToArtAscii();

            AfterSignIn.ComeBack(account, "Survey was filled.");

        }

    }
}
