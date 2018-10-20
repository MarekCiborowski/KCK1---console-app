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
            string _answerValue, 
            int _questionID, 
            Question _question, 
            ICollection<Vote> _vote)
        {
            return new Answer
            {
                answerValue = _answerValue,
                questionID = _questionID,
                question = _question,
                vote = _vote,
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
