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
    public class AnswerRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Answer GetAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.answers.Include(t => t.Votes).FirstOrDefault(a => a.AnswerID == id);
        }

        public void AddAnswer(Answer answer)
        {
            db.answers.Add(answer);
            db.SaveChanges();
        }

        public void EditAnswer(Question editedAnswer)
        {
            db.Entry(editedAnswer).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Answer answer = db.answers.Find(id);
            db.answers.Remove(answer);
            db.SaveChanges();
        }
    }
}
