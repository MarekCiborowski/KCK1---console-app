﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace ServiceLayer.Services
{
    public class AccountSurveyService
    {
        private AccountSurveyRepository accountSurveyR = new AccountSurveyRepository();

       
        public AccountSurvey GetAccountSurvey(int? id)
        {
            return accountSurveyR.GetAccountSurvey(id);
        }

        public void DeleteAccount(int? id)
        {
            accountSurveyR.RemoveAccountSurvey(id);
        }
  
    }
}