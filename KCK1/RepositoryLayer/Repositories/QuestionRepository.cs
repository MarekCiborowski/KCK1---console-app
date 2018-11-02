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
            bool _canAddOwnAnwer,
            bool _isSingleChoice,
            ICollection<Answer> _answer)
        {
            return new Question
            {
                questionValue = _questionValue,
                categoryID = GetQuestionCategory(_canAddOwnAnwer, _isSingleChoice),
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

        public List<Answer> GetAnswers(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            Question question = db.questions.Include(t => t.answer).FirstOrDefault(t => t.questionID == id);

            return question.answer.ToList();
        }

        public int GetQuestionCategory(bool canAddOwnAnswer, bool isSingleChoice)
        {
            Category category = db.categories.FirstOrDefault(t => t.isSingleChoice == isSingleChoice
            && t.canAddOwnAnswer == canAddOwnAnswer);
            if (category == null)
                throw new ArgumentNullException("Category is null");
            return category.categoryID;
        }

        public Category GetQuestionCategory(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.questions.Include(t => t.category).FirstOrDefault(t => t.questionID == id).category;

        }
    }
}
