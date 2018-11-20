using DataTransferObjects.Models;
using RepositoryLayer.Repositories;
using Survey_MVC.Models;
using Survey_MVC.ViewModels.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Survey_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SurveyRepository surveyRepository = new SurveyRepository();
        private AccountRepository accountRepository = new AccountRepository();
        private QuestionRepository questionRepository = new QuestionRepository();
        private int pageSize = 2;
        public ActionResult Index(int page=1)
        {
            Account account = (Account)Session["CurrentUser"];
           
            SurveyListVM surveys  = new SurveyListVM
            {
                surveyList = surveyRepository.GetSurveysToFill(account.accountID)
                .OrderBy(p => p.surveyID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = surveyRepository.GetSurveysToFill(account.accountID).Count
                },
                
            };
            return View(surveys);
        }

        public ActionResult FillSurvey(int? id)
        {
            Account account = (Account)Session["CurrentUser"];
            if (accountRepository.DidFillSurvey(account.accountID, id)) {
                TempData["message"] = "You have already filled this survey.";
                return RedirectToAction("Index");
            }

            Survey survey = surveyRepository.GetSurvey(id);
            SurveyToFillVM surveyToFill = new SurveyToFillVM
            {
                surveyID = survey.surveyID,
                isAnonymous = survey.isAnonymous,
                title = survey.title,
                description = survey.description,
                authorNickname = surveyRepository.GetAuthor(id).nickname
               
            };
            
            foreach(Question question in surveyRepository.GetQuestions(survey.surveyID))
            {
                Category category = questionRepository.GetQuestionCategory(question.questionID);
                QuestionVM questionVM = new QuestionVM
                {
                    questionValue = question.questionValue,
                    canAddOwnAnswers = category.canAddOwnAnswer,
                    isSingleChoice = category.isSingleChoice,
                    questionID = question.questionID
                };
                
                foreach(Answer answer in question.answer)
                {
                    AnswerVM answerVM = new AnswerVM
                    {
                        value = answer.answerValue,
                        answerID = answer.answerID
                        
                    };
                    
                    questionVM.answers.Add(answerVM);
                }
                surveyToFill.questions.Add(questionVM);
            }
            



            return View(surveyToFill);

        }
        [HttpPost][ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FillSurvey (SurveyToFillVM surveyToFillVM, string button)
        {
            if (ModelState.IsValid)
            {

            }
            return View(surveyToFillVM);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult CreateSurvey ()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSurvey(CreateSurveyVM createSurveyVM)
        {
            if (ModelState.IsValid)
            {

            }

            return View(createSurveyVM);
        }

    }
}