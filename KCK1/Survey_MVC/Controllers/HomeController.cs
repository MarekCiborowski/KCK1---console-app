﻿using DataTransferObjects.Models;
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
        private int pageSize = 2;
        public ActionResult Index(int page=1)
        {
            SurveyListVM surveys  = new SurveyListVM
            {
                surveyList = surveyRepository.GetSurveys()
                .OrderBy(p => p.surveyID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = surveyRepository.GetSurveys().Count
                },
                
            };
            return View(surveys);
        }

        public ActionResult ChooseSurvey(int? surveyID)
        {
            Survey survey = surveyRepository.GetSurvey(surveyID);
            SurveyToFill surveyToFill = new SurveyToFill
            {
                surveyID = survey.surveyID,
                isAnonymous = survey.isAnonymous,
                questions = surveyRepository.GetQuestions(survey.surveyID)
            };

            

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}