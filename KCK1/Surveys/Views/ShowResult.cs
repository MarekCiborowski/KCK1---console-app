using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Models;
using RepositoryLayer.Repositories;
using Console = Colorful.Console;
using System.Drawing;

namespace Surveys.Views
{


    public class ShowResult
    {
        private class DisplayedAnswer
        {
            public Answer answer { get; set; }
            public bool isChecked { get; set; } = false;
            public bool isSelected { get; set; } = false;
            public int answerPositionY { get; set; }
            public int answerNumber { get; set; }
            public bool addingOwnQuestion { get; set; } = false;
        }
        private class DisplayedQuestion
        {
            public Question question { get; set; }
            public bool isChecked { get; set; } = false;
            public bool isSelected { get; set; } = false;
            public int questionPositionY { get; set; }
            public int questionNumber { get; set; }
        }

        public static void Show(Account account, Survey survey)
        {
            Console.ForegroundColor = Color.White;
            int positionX = 30, positionY = 15;
            Console.SetCursorPosition(positionX, positionY);

            Console.Write("Title: " + survey.title);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Description: " + survey.description);
            positionY++;
            Console.SetCursorPosition(positionX, positionY);

            int quantityOfVotes = 0;
            Console.Write("Votes: " + quantityOfVotes);
            positionY += 2;
            positionX = 15;


            Configuration.ConsoleClearToArtAscii();
            AfterSignIn.ComeBack(account, "You back to menu.");
        }
    }
}
