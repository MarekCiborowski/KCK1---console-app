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

        }

    }
}
