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
        private AnswerRepository answerRepository = new AnswerRepository();
        private AccountSurveyRepository accountSurveyRepository = new AccountSurveyRepository();

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
        public ActionResult FillSurvey (SurveyToFillVM surveyToFillVM, string button)
        {
            if (button == "Submit")
            {
                bool isValid = true;
                for(int i = 0;i< surveyToFillVM.questions.Count; i++)
                {
                    QuestionVM question = surveyToFillVM.questions[i];
                    if (question.isSingleChoice)
                    {
                        if (question.selectedAnswersID == null) {
                            ModelState.AddModelError(string.Format("questions[{0}].questionValue", i), "Select at least one answer");
                            isValid = false;
                        }
                    }
                    else
                    {
                        if (!question.answers.Any(a => a.isChecked == true))
                        {
                            ModelState.AddModelError(string.Format("questions[{0}].questionValue", i), "Select at least one answer");
                            isValid = false;
                        }
                    }
                }

                if (ModelState.IsValid && isValid)
                {
                    Account account = (Account)Session["CurrentUser"];
                    foreach(QuestionVM question in surveyToFillVM.questions)
                    {
                        if (question.isSingleChoice)
                            answerRepository.AddVoteToAnswer(account.accountID, question.selectedAnswersID.GetValueOrDefault());                     
                        else
                        {
                            foreach(AnswerVM answer in question.answers.Where(a => a.isChecked))
                            {
                                answerRepository.AddVoteToAnswer(account.accountID, answer.answerID);
                            }
                        }
                    }
                    accountSurveyRepository.AddAccountSurvey(account.accountID, surveyToFillVM.surveyID);
                    return RedirectToAction("Index", "Home");
                }
                return View(surveyToFillVM);
            }
            else
            {
                int questionID = Int32.Parse(button);
                string newAnswerValue = surveyToFillVM.questions.FirstOrDefault(t => t.questionID == questionID).newAnswer;
                int questionIndex = surveyToFillVM.questions.IndexOf(surveyToFillVM.questions.
                        FirstOrDefault(t => t.questionID == questionID));
                if (string.IsNullOrEmpty(newAnswerValue))
                {
                    
                    ModelState.AddModelError(string.Format("questions[{0}].newAnswer", questionIndex), "You can't add empty answer.");
                    return View(surveyToFillVM);
                }
                Answer newAnswer = answerRepository.CreateAnswer(surveyToFillVM.questions[questionIndex].newAnswer);
                answerRepository.AddAnswerToQuestion(newAnswer, questionID);

                surveyToFillVM.questions[questionIndex].answers.Add(new AnswerVM { answerID = newAnswer.answerID, value = newAnswerValue });
                surveyToFillVM.questions[questionIndex].canAddOwnAnswers = false;

                return View(surveyToFillVM);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        public ActionResult CreateSurvey ()
        {
            CreateSurveyVM createSurveyVM = new CreateSurveyVM();
            return View(createSurveyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSurvey(CreateSurveyVM createSurveyVM, string button)
        {
            if(button == "AddQuestion")
            {
                createSurveyVM.newQuestion.answers.Add(createSurveyVM.newQuestion.firstAnswerValue);
                createSurveyVM.newQuestion.answers.Add(createSurveyVM.newQuestion.secondAnswerValue);
                createSurveyVM.questions.Add(createSurveyVM.newQuestion);
                return View(createSurveyVM);
            }
            else if(button == "Confirm")
            {
                if (ModelState.IsValid)
                {

                }
            }
            
            return View(createSurveyVM);
        }

    }
}