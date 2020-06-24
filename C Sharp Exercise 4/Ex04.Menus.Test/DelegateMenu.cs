using System;
using Ex04.Menus.Delegates.Items;

namespace Ex04.Menus.Test
{
    public class DelegateMenu
    {
        private readonly MainMenu r_MainMenu;

        public DelegateMenu()
        {
            this.r_MainMenu = new MainMenu("Main Menu");
        }

        public void RunDelegateMenu()
        {
            SubMenuItem versionAndDigits = new SubMenuItem("Version and Digits");
            SubMenuItem dateAndTime = new SubMenuItem("Show Date/Time");
            ActionMenuItem countCapitalsItem = new ActionMenuItem("Count Capitals");
            ActionMenuItem showVersionItem = new ActionMenuItem("Show Version");
            ActionMenuItem showTimeItem = new ActionMenuItem("Show Time");
            ActionMenuItem showDateItem = new ActionMenuItem("Show Date");

            countCapitalsItem.ActionMenuItemChosen += CountCapitalsItem_WasSelected;
            showVersionItem.ActionMenuItemChosen += ShowVersionItem_WasSelected;
            showTimeItem.ActionMenuItemChosen += ShowTimeItem_WasSelected;
            showDateItem.ActionMenuItemChosen += ShowDateItem_WasSelected;
            this.r_MainMenu.AddItem(versionAndDigits);
            this.r_MainMenu.AddItem(dateAndTime);
            versionAndDigits.AddItem(countCapitalsItem);
            versionAndDigits.AddItem(showVersionItem);
            dateAndTime.AddItem(showTimeItem);
            dateAndTime.AddItem(showDateItem);
            this.r_MainMenu.Show();
        }

        private void CountCapitalsItem_WasSelected()
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

        private void ShowVersionItem_WasSelected()
        {
            Console.WriteLine("Version: 20.2.4.30620");
        }

        private void ShowTimeItem_WasSelected()
        {
            Console.WriteLine(string.Format("{0:00}:{1:00}", DateTime.Now.Hour, DateTime.Now.Minute));
        }

        private void ShowDateItem_WasSelected()
        {
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
        }
    }
}