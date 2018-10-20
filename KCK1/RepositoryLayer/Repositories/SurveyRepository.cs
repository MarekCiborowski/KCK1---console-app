using DatabaseLayer;
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

        public Survey GetSurvey(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.surveys.Include(q => q.Question).FirstOrDefault(s => s.SurveyID == id);
        }

        public void AddSurvey(Survey survey)
        {
            db.surveys.Add(survey);
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
    }
}
