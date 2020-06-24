using System;
using Ex04.Menus.Interfaces.IListeners;

namespace Ex04.Menus.Test
{
    public class ActionsClass : IActionsListener
    {
        void IActionsListener.PerformActionMenuItemSelection(int i_ActionID)
        {
            switch (i_ActionID)
            {
                case 1:
                    countCaptials();
                    break;
                case 2:
                    showVersion();
                    break;
                case 3:
                    showTime();
                    break;
                case 4:
                    showDate();
                    break;
            }
        }

        private static void countCaptials()
        {
            int countCaptials = 0;
            string inputString = string.Empty;

            Console.Write("Please enter your sentence: ");
            inputString = Console.ReadLine();
            foreach (char inputChar in inputString)
            {
                if (char.IsUpper(inputChar))
                {
                    countCaptials++;
                }
            }

            Console.WriteLine(string.Format("{1}The amount of captial letters is: {0}", countCaptials, Environment.NewLine));
        }

        private static void showVersion()
        {
            Console.WriteLine("Version: 20.2.4.30620");
        }

        private static void showTime()
        {
            Console.WriteLine(string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute));
        }

        private static void showDate()
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}