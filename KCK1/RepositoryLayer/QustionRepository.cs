using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class QustionRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Question GetQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");

            //to chyba przez wielkość liter te podkreślenie robi
            return db.questions.FirstOrDefault(q => q.QuestionID == id);
        }

        public void AddQuestion(Question question)
        {
            db.questions.Add(question);
            db.SaveChanges();
        }

        public Question EditQuestion(Question editedQuestion)
        {
            Question question = db.answers.Find(editedQuestion.QuestionID);
            question.QuestionValue = editedQuestion.QuestionValue;
            question.CategoryID = editedQuestion.CategoryID;
            question.category = editedQuestion.category;
            db.SaveChanges();
            return question;
        }

        public void RemoveAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Question answer = db.answers.Find(id);
            db.answers.Remove(answer);
            db.SaveChanges();
        }
    }
}
