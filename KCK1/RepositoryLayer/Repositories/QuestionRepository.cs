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
    public class QuestionRepository
    {
        private DatabaseContext db = new DatabaseContext();
        public Question CreateQuestion(
            string _questionValue,
            int _categoryID,
            Category _category,
            int _surveyID,
            Survey _survey,
            ICollection<Answer> _answer)
        {
            return new Question
            {
                questionValue = _questionValue,
                categoryID = _categoryID,
                category = _category,
                surveyID = _surveyID,
                survey = _survey,
                answer = _answer
            };
        }
        public Question GetQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.questions.Include(t => t.answer).Include(t => t.category).FirstOrDefault(q => q.questionID == id);
        }

        public void AddQuestion(Question question)
        {
            db.questions.Add(question);
            db.SaveChanges();
        }

        public void EditQuestion(Question editedQuestion)
        {
            db.Entry(editedQuestion).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Question question = db.questions.Find(id);
            db.questions.Remove(question);
            db.SaveChanges();
        }
    }
}
