using System;
using Ex04.Menus.Delegates.Items;

namespace Ex04.Menus.Delegates
{
    internal class SubMenuItemListener
    {
        public void SubMenuItem_WasSelected(SubMenuItem i_SubMenuItem)
        {
            if (i_SubMenuItem.MenuItemName == "Back")
            {
                (i_SubMenuItem.FatherMenuItem as SubMenuItem).Show();
            }
            else
            {
                if (i_SubMenuItem.MenuItemName == "Quit")
                {
                    Console.Clear();
                    i_SubMenuItem.QuitOptionChosen = true;
                }
                else
                {
                    i_SubMenuItem.Show();
                }
            }
        }
    }
}