using DataTransferObjects.Models;
using RepositoryLayer.Repositories;
using Survey_MVC.Filters;
using Survey_MVC.Models;
using Survey_MVC.ViewModels.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Survey_MVC.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        private SurveyRepository surveyRepository = new SurveyRepository();
        private AccountRepository accountRepository = new AccountRepository();
        private QuestionRepository questionRepository = new QuestionRepository();
        private AnswerRepository answerRepository = new AnswerRepository();
        private AccountSurveyRepository accountSurveyRepository = new AccountSurveyRepository();

        private int pageSize = 5;
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Account account = (Account)Session["CurrentUser"];
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Desc" ? "description_desc" : "Desc";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Survey> surveyList = surveyRepository.GetSurveysToFill(account.accountID);

            surveyList = SortSurveys(surveyList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(surveyList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AuthorSurveys(int id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            Account account = (Account)Session["CurrentUser"];
            if (account.accountID == id)
                return RedirectToAction("MySurveys");

            Session["returnURL"] = Request.UrlReferrer.AbsoluteUri;

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Desc" ? "description_desc" : "Desc";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
            ViewBag.AuthorID = id;
            List<Survey> surveyList = accountRepository.GetAccountAuthorSurveys(id);

            surveyList = SortSurveys(surveyList, sortOrder, searchString, page);


            int pageNumber = (page ?? 1);
            return View(surveyList.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MySurveys(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Session["returnURL"] = Request.UrlReferrer.AbsoluteUri;
            Account account = (Account)Session["CurrentUser"];
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Desc" ? "description_desc" : "Desc";
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Survey> surveyList = accountRepository.GetAccountAuthorSurveys(account.accountID);

            surveyList = SortSurveys(surveyList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(surveyList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult FilledSurveys(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Account account = (Account)Session["CurrentUser"];
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Desc" ? "description_desc" : "Desc";
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Survey> surveyList = accountRepository.GetAccountFilledSurveys(account.accountID);

            surveyList = SortSurveys(surveyList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(surveyList.ToPagedList(pageNumber, pageSize));
        }




        private List<Survey> SortSurveys(List<Survey> surveyList, string sortOrder, string searchString, int? page)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                surveyList = surveyList.Where(s => s.title.Contains(searchString)
                                       || s.description.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "title_desc":
                    surveyList = surveyList.OrderByDescending(s => s.title).ToList();
                    break;
                case "Desc":
                    surveyList = surveyList.OrderBy(s => s.description).ToList();
                    break;
                case "description_desc":
                    surveyList = surveyList.OrderByDescending(s => s.description).ToList();
                    break;
                default:
                    surveyList = surveyList.OrderBy(s => s.title).ToList();
                    break;
            }


            return surveyList;
        }


        public ActionResult FillSurvey(int? id)
        {
            Account account = (Account)Session["CurrentUser"];
            if (accountRepository.DidFillSurvey(account.accountID, id))
            {
                TempData["message"] = "You have already filled this survey.";
                var returnURL = (Session["returnURL"] != null) ? Session["returnURL"].ToString() : Url.Action("Index", "Home");
                return Redirect(returnURL);
            }

            Survey survey = surveyRepository.GetSurvey(id);
            Account author = surveyRepository.GetAuthor(id);
            SurveyToFillVM surveyToFill = new SurveyToFillVM
            {
                surveyID = survey.surveyID,
                isAnonymous = survey.isAnonymous,
                title = survey.title,
                description = survey.description,
                authorNickname = author.nickname,
                accountID = author.accountID

            };

            foreach (Question question in surveyRepository.GetQuestions(survey.surveyID))
            {
                Category category = questionRepository.GetQuestionCategory(question.questionID);
                QuestionVM questionVM = new QuestionVM
                {
                    questionValue = question.questionValue,
                    canAddOwnAnswers = category.canAddOwnAnswer,
                    isSingleChoice = category.isSingleChoice,
                    questionID = question.questionID
                };

                foreach (Answer answer in question.answer)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillSurvey(SurveyToFillVM surveyToFillVM, string button)
        {
            if (button == "Submit")
            {
                bool isValid = true;
                for (int i = 0; i < surveyToFillVM.questions.Count; i++)
                {
                    QuestionVM question = surveyToFillVM.questions[i];
                    if (question.isSingleChoice)
                    {
                        if (question.selectedAnswersID == null)
                        {
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
                    foreach (QuestionVM question in surveyToFillVM.questions)
                    {
                        if (question.isSingleChoice)
                            answerRepository.AddVoteToAnswer(account.accountID, question.selectedAnswersID.GetValueOrDefault());
                        else
                        {
                            foreach (AnswerVM answer in question.answers.Where(a => a.isChecked))
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




        public ActionResult CreateSurvey()
        {
            CreateSurveyVM createSurveyVM = new CreateSurveyVM();
            return View(createSurveyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSurvey(CreateSurveyVM createSurveyVM, string button)
        {
            if (button == "AddQuestion")
            {
                if (createSurveyVM.newQuestion.questionValue == null || createSurveyVM.newQuestion.questionValue == "")
                {
                    ModelState.AddModelError(string.Format("newQuestion.questionValue"), "You can't add empty question.");
                    return View(createSurveyVM);
                }
                createSurveyVM.newQuestion.questionValueCopy = createSurveyVM.newQuestion.questionValue;
                createSurveyVM.questions.Add(createSurveyVM.newQuestion);
                return View(createSurveyVM);
            }
            // Create Survey
            else if (button == "Confirm")
            {
                if (ModelState.IsValid)
                {
                    Account account = (Account)Session["CurrentUser"];
                    int index = 0;
                    bool isOk = true;
                    if (createSurveyVM.title == null || createSurveyVM.title == "")
                    {
                        ModelState.AddModelError(string.Format("title"), "You can't add survey without title.");
                        isOk = false;
                    }
                    if (createSurveyVM.description == null || createSurveyVM.description == "")
                    {
                        ModelState.AddModelError(string.Format("description"), "You can't add survey without description.");
                        isOk = false;
                    }
                    foreach (CreateSurveyVM.NewQuestion question in createSurveyVM.questions)
                    {
                        if (question.answers.Count < 2)
                        {
                            ModelState.AddModelError(string.Format("questions[{0}].questionValue", index), "All questions must have 2 or more answers.");
                            isOk = false;
                        }
                        int i = 0;
                        foreach (string answer in question.answers)
                        {
                            if (answer == "")
                            {
                                ModelState.AddModelError(string.Format("questions[{0}].answers[{1}]", index, i), "You can't add empty answer.");
                                isOk = false;
                            }
                            i++;
                        }
                        if (question.questionValue == null || question.questionValue == "")
                        {
                            ModelState.AddModelError(string.Format("questions[{0}].questionValue", index), "You can't add empty question.");
                            isOk = false;
                        }
                        index++;
                    }
                    if (!isOk)
                        return View(createSurveyVM);

                    ICollection<Question> questions = new List<Question>();
                    foreach (CreateSurveyVM.NewQuestion question in createSurveyVM.questions)
                    {
                        ICollection<Answer> answers = new List<Answer>();
                        foreach (string answer in question.answers)
                        {
                            Answer answerCreated = answerRepository.CreateAnswer(answer);
                            answers.Add(answerCreated);
                        }
                        Question questionCreated = questionRepository.CreateQuestion(question.questionValue, question.canAddOwnAnswers, question.isSingleChoice, answers);
                        questions.Add(questionCreated);
                    }
                    Survey survey = surveyRepository.CreateSurvey(createSurveyVM.title, createSurveyVM.description, createSurveyVM.isAnonymous, questions);
                    surveyRepository.AddSurvey(survey, account);
                    return RedirectToAction("Index", "Home");
                }
                return View(createSurveyVM);
            }
            // Delete Answer
            else if (button.Contains("DeleteAnswer"))
            {
                string indexI = "", indexJ = "";
                int i = 0;
                for (i = 13; i < button.Length; i++)
                {
                    string slice = button.Substring(i, 1);
                    if (string.Equals(slice, " "))
                        break;
                    indexI = slice;
                }
                indexJ = button.Substring(13 + indexI.Length + 1);
                i = Int32.Parse(indexI);
                int j = Int32.Parse(indexJ);
                createSurveyVM.questions[i].answers.RemoveAt(j);
                createSurveyVM.questions[i].answersCopy.RemoveAt(j);
            }

            // Delete Question
            else if (button.Contains("DeleteQuestion"))
            {
                string index = button.Substring(15);
                int i = Int32.Parse(index);
                createSurveyVM.questions.RemoveAt(i);
            }

            // Add Answer and validation
            else
            {
                int index = Int32.Parse(button);
                string newAnswerValue = createSurveyVM.questions[index].newAnswer;
                bool isOk = true;
                if (string.IsNullOrEmpty(newAnswerValue))
                {
                    ModelState.AddModelError(string.Format("questions[{0}].newAnswer", index), "You can't add empty answer.");
                    isOk = false;
                }
                if (isOk)
                {
                    createSurveyVM.questions[index].answers.Add(newAnswerValue);
                    createSurveyVM.questions[index].answersCopy.Add(newAnswerValue);
                }
                isOk = true;
                int i = 0;
                foreach (string answer in createSurveyVM.questions[index].answers)
                {
                    if (answer == "")
                    {
                        ModelState.AddModelError(string.Format("questions[{0}].answers[{1}]", index, i), "You can't add empty answer.");
                        isOk = false;
                    }
                    i++;
                }
                if (createSurveyVM.questions[index].questionValue == null)
                {
                    ModelState.AddModelError(string.Format("questions[{0}].questionValue", index), "You can't add empty question.");
                    isOk = false;
                }
                if (!isOk)
                {
                    createSurveyVM.questions[index].answers.Clear();
                    createSurveyVM.questions[index].answers.AddRange(createSurveyVM.questions[index].answersCopy);
                    createSurveyVM.questions[index].questionValue =
                        createSurveyVM.questions[index].questionValueCopy;
                }
                else
                {
                    createSurveyVM.questions[index].answersCopy.Clear();
                    createSurveyVM.questions[index].answersCopy.AddRange(createSurveyVM.questions[index].answers);
                    createSurveyVM.questions[index].questionValueCopy =
                        createSurveyVM.questions[index].questionValue;
                }
            }
            return View(createSurveyVM);
        }

        public ActionResult SurveyResults(int id)
        {
            Account author = surveyRepository.GetAuthor(id);
            Survey survey = surveyRepository.GetSurvey(id);

            SurveyResultsVM surveyResults = new SurveyResultsVM
            {
                authorNickname = author.nickname,
                authorID = author.accountID,
                title = survey.title,
                numberOfVoters = surveyRepository.GetQuantityOfVoters(id),
                questions = new List<QuestionResultsVM>()
            };

            foreach (Question question in surveyRepository.GetQuestions(id))
            {
                surveyResults.questions.Add(new QuestionResultsVM
                {
                    questionID = question.questionID,
                    questionValue = question.questionValue
                });
            }

            return View(surveyResults);
        }

        public ActionResult QuestionResults(int id)
        {
            Question question = questionRepository.GetQuestion(id);
            Survey survey = surveyRepository.GetSurvey(question.surveyID);
            QuestionResultsVM questionResults = new QuestionResultsVM
            {
                questionValue = question.questionValue,
                isAnonymous = surveyRepository.IsAnonymous(survey.surveyID),
                answers = new List<AnswerVotersVM>()
            };

            foreach (Answer answer in questionRepository.GetAnswers(id))
            {
                questionResults.answers.Add(new AnswerVotersVM
                {
                    answerID = answer.answerID,
                    answerValue = answer.answerValue,
                    numberOfVotes = answerRepository.GetQuantityOfVotes(answer.answerID),
                });
            }


            return View(questionResults);
        }

        public ActionResult AnswerVoters(int id)
        {
            Answer answer = answerRepository.GetAnswer(id);
            //próba wejścia po adresie url
            if (surveyRepository.GetSurvey(questionRepository.GetQuestion(answer.questionID).surveyID).isAnonymous)
            {
                TempData["message"] = "Results of this survey are anonymous.";
                return RedirectToAction("QuestionResults", new { id = answerRepository.GetAnswer(id).questionID });
            }

            AnswerVotersVM answerVoters = new AnswerVotersVM
            {
                answerValue = answer.answerValue,
                numberOfVotes = answerRepository.GetQuantityOfVotes(answer.answerID),
                voters = new List<VoterVM>()
            };
            foreach(Account voter in answerRepository.GetAccountsVoters(answer.answerID))
            {
                answerVoters.voters.Add(new VoterVM
                {
                    voterID = voter.accountID,
                    email = voter.email,
                    nickname = voter.nickname
                });
            }

            return View(answerVoters);
        }
        public ActionResult DeleteSurvey(int id)
        {
            Survey survey = surveyRepository.GetSurvey(id);

            DeleteSurveyVM deleteSurveyVM = new DeleteSurveyVM
            {
                surveyID = id,
                surveyTitle = survey.title

            };
            
            return View(deleteSurveyVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSurvey(DeleteSurveyVM deleteSurveyVM)
        {
            var returnURL = (Session["returnURL"] != null) ? Session["returnURL"].ToString() : Url.Action("Index", "Home");
            Account account = (Account)Session["CurrentUser"];
            if (surveyRepository.GetAuthor(deleteSurveyVM.surveyID).accountID != account.accountID)
            {
                TempData["message"] = "You cannot delete other users' surveys.";
                
                return Redirect(returnURL);
            }

            surveyRepository.RemoveSurvey(deleteSurveyVM.surveyID);
            TempData["message"] = "Survey was successfully deleted!";
            return RedirectToAction("MySurveys");
        }
    }
}