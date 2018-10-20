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

        public Question GetQuestion(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");

            
            return db.questions.Include(t => t.Answer).Include(t => t.category).FirstOrDefault(q => q.QuestionID == id);
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
