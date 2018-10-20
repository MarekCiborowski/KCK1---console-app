using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Services
{
    public class QuestionService
    {
        private QuestionRepository questionR = new QuestionRepository();

        public Question CreateQuestion(
            string _questionValue,
            int _categoryID,
            Category _category,
            int _surveyID,
            Survey _survey,
            ICollection<Answer> _answer)
        {
            return new Question
            {
                questionValue = _questionValue,
                categoryID = _categoryID,
                category = _category,
                surveyID = _surveyID,
                survey = _survey,
                answer = _answer
            };
        }
        public Question GetQuestion(int? id)
        {
            return questionR.GetQuestion(id);
        }

        public void AddQuestion(Question question)
        {
            questionR.AddQuestion(question);
        }

        public void DeleteQustion(int? id)
        {
            questionR.RemoveQuestion(id);
        }

        public void EditQuestion(Question question)
        {
            questionR.EditQuestion(question);
        }

    }
}
