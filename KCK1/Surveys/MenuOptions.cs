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
            MenuChangePassword menuChangePassword = new MenuChangePassword(account);
            options.Add(menuChangePassword);
            options.Add(new MenuLogout());
            options.Add(new MenuExit());
            return options;
        }


    }
}
