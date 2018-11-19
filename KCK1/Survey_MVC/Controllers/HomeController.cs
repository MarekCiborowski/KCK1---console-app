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
        private int pageSize = 2;
        public ActionResult Index(int page=1)
        {
            Account account = (Account)Session["CurrentUser"];
           
            SurveyListVM surveys  = new SurveyListVM
            {
                surveyList = surveyRepository.GetSurveys(account.accountID)
                .OrderBy(p => p.surveyID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = surveyRepository.GetSurveys(account.accountID).Count
                },
                
            };
            return View(surveys);
        }

        public ActionResult ChooseSurvey(int? id)
        {
            Account account = (Account)Session["CurrentUser"];
            if (accountRepository.DidFillSurvey(account.accountID, id)) {
                TempData["message"] = "You have already filled this survey.";
                return RedirectToAction("Index");
            }

            Survey survey = surveyRepository.GetSurvey(id);
            SurveyToFill surveyToFill = new SurveyToFill()
            {
                surveyID = survey.surveyID,
                isAnonymous = survey.isAnonymous,
                questions = surveyRepository.GetQuestions(survey.surveyID)
            };
            //cos bedzie xd


            return RedirectToAction("Index");

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}