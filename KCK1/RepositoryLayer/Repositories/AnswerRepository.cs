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
            Question question = db.questions.Include(t =>t.answer).FirstOrDefault(t => t.questionID==questionID);
            question.answer.Add(answer);
            db.Entry(question).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void EditAnswer(Answer editedAnswer)
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
        public void AddVoteToAnswer(int _accountID, int _answerID)
        {
            Vote vote = new Vote
            {
                accountID = _accountID,
                answerID = _answerID

            };
            db.votes.Add(vote);
            db.SaveChanges();
        }

        // Returns tuple with 2 defined values:
        // value1: All the votes,
        // value2: Voters[if survey isn't anonymous, then it returns null]
        public Tuple<int, List<Account>> GetAllVotes(int? idAnswer)
        {
            if (idAnswer == null)
                throw new ArgumentNullException("Null argument");

            Answer answer = db.answers.Include(t => t.vote).FirstOrDefault(t => t.answerID == idAnswer);
            Question question = db.questions.Find(answer.questionID);
            Survey survey = db.surveys.Find(question.surveyID);

            SurveyRepository surveyRepository = new SurveyRepository();

            ICollection<Vote> votes = answer.vote;

            if (surveyRepository.IsAnonymous(survey.surveyID))
            {
                List<Account> votersAccounts = db.accounts.Where
                    (t => votes.Select(b => b.accountID).Contains(t.accountID)).ToList();

                return new Tuple<int, List<Account>>(votes.Count, votersAccounts);
            }
            else
            {
                return new Tuple<int, List<Account>>(votes.Count, null);
            }
        }
    }
}
