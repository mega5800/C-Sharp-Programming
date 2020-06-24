using System.Collections.Generic;
using Ex04.Menus.Interfaces.Items;

namespace Ex04.Menus.Test
{
    public class InterfaceMenu
    {
        private readonly MainMenu r_MainMenu;

        public InterfaceMenu()
        {
            this.r_MainMenu = new MainMenu("Main Menu");
        }

        public void RunInterfaceMenu()
        {
            ActionsClass actionsClass = new ActionsClass();
            SubMenuItem versionAndDigits = new SubMenuItem("Version and Digits");
            SubMenuItem dateAndTime = new SubMenuItem("Show Date/Time");
            ActionMenuItem countCapitals = new ActionMenuItem("Count Capitals", 1);
            ActionMenuItem showVersion = new ActionMenuItem("Show Version", 2);
            ActionMenuItem showTime = new ActionMenuItem("Show Time", 3);
            ActionMenuItem showDate = new ActionMenuItem("Show Date", 4);

            countCapitals.ActionsListener = actionsClass;
            showVersion.ActionsListener = actionsClass;
            showTime.ActionsListener = actionsClass;
            showDate.ActionsListener = actionsClass;
            this.r_MainMenu.AddItem(versionAndDigits);
            this.r_MainMenu.AddItem(dateAndTime);
            versionAndDigits.AddItem(countCapitals);
            versionAndDigits.AddItem(showVersion);
            dateAndTime.AddItem(showTime);
            dateAndTime.AddItem(showDate);
            this.r_MainMenu.Show();
        }
    }
}