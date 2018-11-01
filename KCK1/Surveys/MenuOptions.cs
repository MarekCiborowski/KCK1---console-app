using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Views;
using DatabaseLayer.Models;
namespace Surveys
{
    public interface MenuOptions
    {
        void OptionFunction();
        string GetName();
        string GetDescription();
    }

    public class MenuCreateAccount : MenuOptions
    {
        public void OptionFunction()
        {
            Console.Clear();
            CreateAccount.Create();
        }

        public string GetName()
        {
            return "     Create Account    ";
        }

        public string GetDescription()
        {
            return "";
        }
    }

    public class MenuSignIn : MenuOptions
    {
        public void OptionFunction()
        {
            Console.Clear();
            SignInView.SignIn();
        }

        public string GetName()
        {
            return "        Sign In        ";
        }

        public string GetDescription()
        {
            return "";
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
            return "         Exit         ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
            ChangePassword.Change(account);
        }

        public string GetName()
        {
            return "     Change Password     ";
        }

        public string GetDescription()
        {
            return "";
        }
    }

    public class MenuLogout : MenuOptions
    {
        public void OptionFunction()
        {
            Console.Clear();
            Program.Start("Logout successful");
        }

        public string GetName()
        {
            return "        Logout         ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
            //CreateSurvey.Create(account);
        }

        public string GetName()
        {
            return "      Create Survey      ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
            ShowFollowed.Show(account);
        }

        public string GetName()
        {
            return "    Show Followed Users    ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
            //ShowFollowing.Show(account);
        }

        public string GetName()
        {
            return "    Show Following Users     ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
            //ShowMySurveys.Show(account);
        }

        public string GetName()
        {
            return "    Show My Surveys     ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
           // ShowCompletedSurveys.Show(account);
        }

        public string GetName()
        {
            return "  Show Completed Surveys  ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
          //  ShowAllSurveys.Show(account);
        }

        public string GetName()
        {
            return "    Show All Surveys    ";
        }

        public string GetDescription()
        {
            return "";
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
            Console.Clear();
          //  ShowAllPeople.Show(account);
        }

        public string GetName()
        {
            return "    Show All People    ";
        }

        public string GetDescription()
        {
            return "";
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
