using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Views;
using RepositoryLayer.Repositories;
using DataTransferObjects.Models;

namespace Surveys
{
    public interface MenuOptions
    {
        void OptionFunction();
        string GetName();
    }

    public class MenuCreateAccount : MenuOptions
    {
        public void OptionFunction()
        {
            CreateAccount.Create();
        }

        public string GetName()
        {
            return "         Create Account        ";
        }
    }

    public class MenuSignIn : MenuOptions
    {
        public void OptionFunction()
        {
            SignInView.SignIn();
        }

        public string GetName()
        {
            return "            Sign In            ";
        }
    }

    public class MenuExit : MenuOptions
    {
        public void OptionFunction()
        {
            Environment.Exit(0);
        }

        public string GetName()
        {
            return "              Exit             ";
        }
    }

    public class MenuChangePassword : MenuOptions
    {
        private Account account;
        public MenuChangePassword(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChangePassword.Change(account);
        }

        public string GetName()
        {
            return "        Change Password        ";
        }
    }

    public class MenuLogout : MenuOptions
    {
        public void OptionFunction()
        {
            Program.ComeBack("Logout successful");
        }

        public string GetName()
        {
            return "             Logout            ";
        }
    }

    public class MenuCreateSurvey : MenuOptions
    {
        private Account account;
        public MenuCreateSurvey(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            CreateSurvey.Create(account);
        }

        public string GetName()
        {
            return "         Create Survey         ";

        }
    }

    public class MenuShowFollowed : MenuOptions
    {
        private Account account;
        public MenuShowFollowed(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseAccount.Choose(account, new AccountRepository().GetFollowedAccounts(account.accountID));
        }

        public string GetName()
        {
            return "      Show Followed Users      ";
        }
    }

    public class MenuShowFollowing : MenuOptions
    {
        private Account account;
        public MenuShowFollowing(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseAccount.Choose(account, new AccountRepository().GetFollowingAccounts(account.accountID));
        }

        public string GetName()
        {
            return "     Show Following Users      ";
        }
    }

    public class MenuShowMySurveys : MenuOptions
    {
        private Account account;
        public MenuShowMySurveys(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseSurvey.Choose(account, new AccountRepository().GetAccountAuthorSurveys(account.accountID));
        }

        public string GetName()
        {
            return "        Show My Surveys        ";
        }
    }

    public class MenuShowCompletedSurveys : MenuOptions
    {
        private Account account;
        public MenuShowCompletedSurveys(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseSurvey.Choose(account, new AccountRepository().GetAccountFilledSurveys(account.accountID));
        }

        public string GetName()
        {
            return "     Show Completed Surveys    ";
        }
    }

    public class MenuShowAllSurveys : MenuOptions
    {
        private Account account;
        public MenuShowAllSurveys(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseSurvey.Choose(account, new SurveyRepository().GetSurveys());
        }

        public string GetName()
        {
            return "        Show All Surveys       ";
        }
    }

    public class MenuShowAllPeople : MenuOptions
    {
        private Account account;
        public MenuShowAllPeople(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            ChooseAccount.Choose(account, new AccountRepository().GetAccountsToList());
        }

        public string GetName()
        {
            return "        Show All People        ";
        }
    }

    public class MenuDeleteAccount : MenuOptions
    {
        private Account account;
        public MenuDeleteAccount(Account _account)
        {
            account = _account;
        }

        public void OptionFunction()
        {
            DeleteAccount.Delete(account);
        }

        public string GetName()
        {
            return "         Delete Account         ";
        }
    }

    public static class Options
    {
        public static List<MenuOptions> GetMainOptions()
        {
            List<MenuOptions> options = new List<MenuOptions>();
            options.Add(new MenuCreateAccount());
            options.Add(new MenuSignIn());
            options.Add(new MenuExit());

            return options;
        }

        public static List<MenuOptions> GetOptionsAfterSignIn(Account account)
        {
            List<MenuOptions> options = new List<MenuOptions>();
            options.Add(new MenuChangePassword(account));

            options.Add(new MenuLogout());

            options.Add(new MenuCreateSurvey(account));

            options.Add(new MenuDeleteAccount(account));

            options.Add(new MenuShowFollowed(account));

            options.Add(new MenuShowFollowing(account));

            options.Add(new MenuShowMySurveys(account));

            options.Add(new MenuShowCompletedSurveys(account));

            options.Add(new MenuShowAllSurveys(account));

            options.Add(new MenuShowAllPeople(account));

            options.Add(new MenuExit());
            return options;
        }
    }
}
