using System;
using Ex04.Menus.Interfaces.IListeners;
using Ex04.Menus.Interfaces.Items;

namespace Ex04.Menus.Interfaces
{
    internal class SubMenuItemListener : ISelectedMenuItemListener
    {
        void ISelectedMenuItemListener.PerformSubMenuItemSelection(SubMenuItem i_SubMenuItem)
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