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

        

        public Answer GetAnswer(int? id)
        {
            return answerR.GetAnswer(id);
        }

        public void AddAnswer(Answer answer)
        {
            answerR.AddAnswer(answer);
        }

        public void DeleteAnswer(int? id)
        {
            answerR.RemoveAnswer(id);
        }

        public void EditAnswer(Answer answer)
        {
            answerR.EditAnswer(answer);
        }
    }
}
