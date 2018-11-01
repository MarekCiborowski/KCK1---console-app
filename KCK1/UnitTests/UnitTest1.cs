using System;
using DatabaseLayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryLayer.Repositories;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private AccountRepository accountRepository;
        private AccountSurveyRepository accountSurveyRepository;
        private AnswerRepository answerRepository;
        private PersonDataRepository personDataRepository;
        private QuestionRepository questionRepository;
        private SurveyRepository surveyRepository;
        [TestInitialize]
        public void TestInitializer()
        {
            accountRepository = new AccountRepository();
            accountSurveyRepository = new AccountSurveyRepository();
            answerRepository = new AnswerRepository();
            personDataRepository = new PersonDataRepository();
            questionRepository = new QuestionRepository();
            surveyRepository = new SurveyRepository();

        }

        [TestMethod]
        public void IsEverythingAllright()
        {
            UserSecurity userSecurity = accountRepository.CreateUserSecurity("login", "password");
            PersonData personData = personDataRepository.CreatePersonData("adres", "miasto", "19-100", "state", "country");
            Account account = accountRepository.CreateAccount(personData, "email", "nick", userSecurity);
            int accountID = accountRepository.AddAccount(account);
            
        }
    }
}
