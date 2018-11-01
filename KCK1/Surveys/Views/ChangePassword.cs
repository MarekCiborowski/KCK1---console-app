using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;

namespace Surveys.Views
{
    public class ChangePassword
    {
        public static void Change(Account account)
        {
            Configuration.setConsoleSize();

            Console.WriteLine(ArtAscii.GetMainTitleString());
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("New password: ");
            positionY++;
            string newPassword = "";
            newPassword = Console.ReadLine();
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Repeat password: ");
            string repeat = "";
            repeat = Console.ReadLine();

            if(newPassword == repeat)
            {
                UserSecurityRepository userSecurityRepo = new UserSecurityRepository();
                UserSecurity userSecurity = userSecurityRepo.GetSecurity(account.personData.personDataID);
                userSecurity.password = repeat;
                userSecurityRepo.EditUserSecurity(userSecurity);

                AfterSignIn.ComeBack(account, "Password was changed.");
            }
               
            AfterSignIn.ComeBack(account, "Passwords were different. Try again.");
        }
    }
}
