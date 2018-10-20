using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class SurveyRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Survey GetSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.surveys.FirstOrDefault(s => s.SurveyID == id);
        }

        public void AddSurvey(Survey survey)
        {
            db.surveys.Add(survey);
            db.SaveChanges();
        }

        public Survey EditSurvey(Survey editedSurvey)
        {
            Survey survey = db.surveys.Find(editedSurvey.SurveyID);
            survey.Title = editedSurvey.Title;
            survey.Description = editedSurvey.Description;
            survey.Question = editedSurvey.Question;
            
            db.SaveChanges();
            return survey;
        }

        
        public void RemoveSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Survey survey = db.surveys.Find(id);

            foreach (Question question in db.questions)
            {
                if (question.QuestionID == id)
                    RemoveQuestion(id);
            }

            foreach (AccountSurvey accountSurvey in survey.AccountSurvey)
            {
                AccountSurveyRepository asr = new AccountSurveyRepository();
                asr.RemoveAccountSurvey(accountSurvey.AccountSurveyID);
            }

            db.surveys.Remove(survey);
            db.SaveChanges();
        }
    }
}
