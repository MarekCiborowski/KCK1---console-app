﻿using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class SurveyRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Survey CreateSurvey(
            string _title,
            string _description,
            bool _isAnonymous,
            ICollection<Question> _question
            )
        {
            return new Survey
            {
                title = _title,
                description = _description,
                isAnonymous = _isAnonymous,
                question = _question,   
            };
        }
        public Survey GetSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.surveys.Include(q => q.question).FirstOrDefault(s => s.surveyID == id);
        }

        public void AddSurvey(Survey survey, Account Author)
        {
            AccountSurvey accountSurvey = new AccountSurvey
            {
                account = Author,
                isAuthor = true,
                survey = survey
            };
            db.accountsSurveys.Add(accountSurvey);
            
            db.SaveChanges();
        }

        public void EditSurvey(Survey editedSurvey)
        {
            db.Entry(editedSurvey).State = EntityState.Modified;
            db.SaveChanges();
        }

        
        public void RemoveSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Survey survey = db.surveys.Find(id);

            db.surveys.Remove(survey);
            db.SaveChanges();
        }

        public bool IsAnonymous(int? id)
        {
            if (id == null)
                throw new ArgumentException("Null argument");
            Survey survey = db.surveys.Find(id);

            return survey.isAnonymous;
        }

        public List<Question> GetQuestions(int? id)
        {
            if (id == null)
                throw new ArgumentException("Null argument");

            Survey survey = db.surveys.Include(t => t.question).FirstOrDefault(t=> t.surveyID==id);

            return survey.question.ToList();
        }
    }
}
