﻿using DatabaseLayer;
using DataTransferObjects.Models;
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
            return db.surveys.FirstOrDefault(s => s.surveyID == id);
        }

        public List<Survey> GetSurveysToFill(int accountID)
        {


            return db.surveys.Where(a => !db.accountsSurveys.Where(b =>
            b.accountID == accountID).Select(d =>
            d.surveyID).Contains(a.surveyID)).ToList();
        }

        public void AddSurvey(Survey survey, Account Author)
        {

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.surveys.Add(survey);
                    AccountSurvey accountSurvey = new AccountSurvey
                    {
                        accountID = Author.accountID,
                        isAuthor = true,
                        surveyID = survey.surveyID
                    };
                    db.accountsSurveys.Add(accountSurvey);

                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //wysypywało się gdy któraś walidacja sie nie zgadzala
                    //in other cases works fine 
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void EditSurvey(Survey editedSurvey)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedSurvey).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public Account GetAuthor(int? SurveyId)
        {
            if (SurveyId == null)
                throw new ArgumentNullException("Null argument");

            Account account = db.accounts.FirstOrDefault(a =>
            a.accountID == db.accountsSurveys.FirstOrDefault(b =>
            b.surveyID == SurveyId && b.isAuthor).accountID);

            return account;
        }
            

        
        public void RemoveSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Survey survey = db.surveys.Find(id);

                    db.surveys.Remove(survey);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
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

            List<Question> questions = db.questions.Include(a => a.answer).Where(t => t.surveyID == id).ToList();

            return questions;
        }

        public int GetQuantityOfVoters(int? surveyID)
        {
            if (surveyID == null)
                throw new ArgumentNullException("Null argument");

            Survey survey = db.surveys.Find(surveyID);

            List<AccountSurvey> accountSurveys = db.accountsSurveys
                .Where(a => a.surveyID == surveyID && a.isAuthor == false).ToList();

            return accountSurveys.Count();

        }
    }
}
