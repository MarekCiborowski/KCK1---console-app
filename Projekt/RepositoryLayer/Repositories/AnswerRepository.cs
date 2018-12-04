using DatabaseLayer;
using DataTransferObjects.Models;
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

        public Answer CreateAnswer(
            string _answerValue           
            )
        {
            return new Answer
            {
                answerValue = _answerValue, 
            };
        }
        public Answer GetAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            return db.answers.Include(t => t.vote).FirstOrDefault(a => a.answerID == id);
        }

        public void AddAnswerToQuestion(Answer answer, int? questionID)
        {
            if (questionID == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Question question = db.questions.Include(t => t.answer).FirstOrDefault(t => t.questionID == questionID);
                    question.answer.Add(answer);
                    db.Entry(question).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void EditAnswer(Answer editedAnswer)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedAnswer).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {

                    dbContextTransaction.Rollback();
                }
            }
        }

        public void RemoveAnswer(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Null argument");
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    Answer answer = db.answers.Find(id);
                    db.answers.Remove(answer);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }
        public void AddVoteToAnswer(int _accountID, int _answerID)
        {
            Vote vote = new Vote
            {
                accountID = _accountID,
                answerID = _answerID

            };
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.votes.Add(vote);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public int GetQuantityOfVotes(int? answerID)
        {
            if (answerID == null)
                throw new ArgumentNullException("Null argument");

            return db.answers.Include(t => t.vote).FirstOrDefault(t => t.answerID == answerID).vote.Count;
            
        }

        public List<Account> GetAccountsVoters(int? answerID)
        {
            if (answerID == null)
                throw new ArgumentNullException("Null argument");
            
            List<Account> votersAccounts = db.accounts.Where(a => db.votes.Where(b =>
            b.answerID == answerID).Select(t => t.accountID).Contains(a.accountID)).ToList();

            return votersAccounts;
        }
    }
}
