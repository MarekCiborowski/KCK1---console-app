using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
namespace ServiceLayer.Services
{
    public class AnswerService
    {
        private AnswerRepository answerR = new AnswerRepository();

        public Answer CreateAnswer(
            string answerValue, 
            int questionID, 
            Question _question, 
            ICollection<Votes> votes)
        {
            return new Answer
            {
                AnswerValue = answerValue,
                QuestionID = questionID,
                question = _question,
                Votes = votes,
            };
        }

        public void AddAnswer(Answer answer)
        {
            answerR.AddAnswer(answer);
        }

        public void DeleteAccount(int? id)
        {
            answerR.RemoveAnswer(id);
        }

        public void EditAccount(Answer answer)
        {
            answerR.EditAnswer(answer);
        }
    }
}
