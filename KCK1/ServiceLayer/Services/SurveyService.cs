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

        public Survey CreateSurvey(
            string _title,
            string _description,
            ICollection<Question> _question,
            ICollection<AccountSurvey> _accountSurvey)
        {
            return new Survey
            {
                title = _title,
                description = _description,
                question = _question,
                //o co chodzi?
                accountSurvey = _accountSurvey
            };
        }

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
