using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.Views
{
    public class DisplayedSurvey
    {
        public Survey survey { get; set; }
        public bool isSelected { get; set; } = false;
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
            displayedSurveys.Find(t => t.surveyNumber == 1).isSelected = true;
            while (true)
            {

            }
        }
            
    }
}
