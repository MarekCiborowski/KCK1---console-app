using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surveys.Views;

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
            Configuration.MainMenu(Options.GetOptions());
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

    public static class Options
    {

        public static List<MenuOptions> GetOptions()
        {
            List<MenuOptions> options = new List<MenuOptions>();
            options.Add(new MenuCreateAccount());
            options.Add(new MenuSignIn());
            options.Add(new MenuExit());
            //options.Add(new MainMenu_ExitGame());

            return options;
        }
    }
}
