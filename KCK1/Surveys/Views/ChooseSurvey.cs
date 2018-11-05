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
    public class DisplayedSurvey
    {
        public Survey survey { get; set; }
        
        public int surveyPositionY { get; set; }
        public int surveyNumber { get; set; }
    }

    public class ChooseSurvey
    {
        public static void Choose(Account account, List<Survey> surveys)
        {
            SurveyRepository surveyRepository = new SurveyRepository();
            QuestionRepository questionRepository = new QuestionRepository();
            AnswerRepository answerRepository = new AnswerRepository();
            AccountRepository accountRepository = new AccountRepository();

            Configuration.SetConsoleSize();

              Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30;


            List<DisplayedSurvey> displayedSurveys = new List<DisplayedSurvey>();
            int currentSurveyPosition=15, currentSurveyNumber=1;
            foreach(Survey survey in surveys)
            {
                displayedSurveys.Add(new DisplayedSurvey {
                    survey = survey, surveyNumber=currentSurveyNumber, surveyPositionY=currentSurveyPosition });
                currentSurveyPosition++;
                currentSurveyNumber++;
            }
            ConsoleKey key;
            int currentlySelectedSurvey = 1, lastSurveyNumber=currentSurveyNumber-1;
            while (true)
            {
                foreach(DisplayedSurvey displayedSurvey in displayedSurveys)
                {
                    Console.SetCursorPosition(positionX, displayedSurvey.surveyPositionY);
                    if (currentlySelectedSurvey == displayedSurvey.surveyNumber)
                        Console.ForegroundColor = Color.Red;
                    else
                        Console.ForegroundColor = Color.White;

                    Console.WriteLine(displayedSurvey.survey.title);


                }
                key = ConsoleKey.B;
                if (Console.KeyAvailable)
                    key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        currentlySelectedSurvey--;
                        if (currentlySelectedSurvey == 0)
                            currentlySelectedSurvey = lastSurveyNumber;
                        break;

                    case ConsoleKey.DownArrow:
                        currentlySelectedSurvey++;
                        if (currentlySelectedSurvey == lastSurveyNumber+1)
                            currentlySelectedSurvey = 1;
                        break;

                    case ConsoleKey.Enter:
                        Configuration.ConsoleClearToArtAscii();
                        //author
                        Survey survey = displayedSurveys.Find(t => t.surveyNumber == currentlySelectedSurvey).survey;
                        if (surveyRepository.GetAuthor(survey.surveyID).accountID == account.accountID)
                            ShowResult.Show(account, survey);
                        // completed survey
                        else if (accountRepository.DidFillSurvey(account.accountID, survey.surveyID))
                            AfterSignIn.ComeBack(account, " You have already filled this survey ");
                        //not yet completed survey
                        else
                            FillSurvey.Fill(account, displayedSurveys.Find(t => t.surveyNumber == currentlySelectedSurvey).survey);

                        break;
                }
            }
        }
            
    }
}
