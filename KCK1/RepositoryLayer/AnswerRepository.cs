using DatabaseLayer;
using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class AnswerRepository
    {
        private DatabaseContext db = new DatabaseContext();

        public Question GetAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.answers.FirstOrDefault(a => a.AnswerID == id);
        }

        public void AddAnswer(Question answer)
        {
            db.answers.Add(answer);
            db.SaveChanges();
        }

        public Question EditAnswer(Question editedAnswer)
        {
            Question answer = db.answers.Find(editedAnswer.AnswerID);
            answer.AnswerValue = editedAnswer.AnswerValue;
            db.SaveChanges();
            return answer;
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
