using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
namespace ServiceLayer.Services
{
    public class SurveyService
    {
        private SurveyRepository surveyR = new SurveyRepository();

        

        public Survey GetAccount(int? id)
        {
            return surveyR.GetSurvey(id);
        }

        public void AddAccount(Survey survey)
        {
            surveyR.AddSurvey(survey);
        }

        public void DeleteAccount(int? id)
        {
            surveyR.RemoveSurvey(id);
        }

        public void EditAccount(Survey account)
        {
            surveyR.EditSurvey(account);
        }
    }
}
