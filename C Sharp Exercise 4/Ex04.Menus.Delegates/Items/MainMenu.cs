using System.Collections.Generic;

namespace Ex04.Menus.Delegates.Items
{
    public sealed class MainMenu : SubMenuItem
    {
        public MainMenu(string i_MenuName) : base(i_MenuName)
        {
            this.m_FatherMenuItem = this;
        }

        public override void AddItem(MenuItem i_InputMenuItem)
        {
            if (this.m_MenuItemList == null)
            {
                this.m_MenuItemList = new List<MenuItem>();
                this.m_MenuItemList.Add(new SubMenuItem("Quit"));
            }

            i_InputMenuItem.FatherMenuItem = this;
            this.m_MenuItemList.Add(i_InputMenuItem);
        }
    }
}