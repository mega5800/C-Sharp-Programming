using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates.Items
{
    public delegate void SubMenuItemDelegate(SubMenuItem i_SubMenuItem);

    public class SubMenuItem : MenuItem
    {
        protected List<MenuItem> m_MenuItemList;
        private static readonly SubMenuItemListener sr_SubMenuItemListener = new SubMenuItemListener();

        public event SubMenuItemDelegate SubMenuItemChosen;

        public SubMenuItem(string i_MenuItemName) : base(i_MenuItemName)
        {
            this.SubMenuItemChosen += sr_SubMenuItemListener.SubMenuItem_WasSelected;
        }

        public List<MenuItem> MenuItemList
        {
            get { return this.m_MenuItemList; }
            set { this.m_MenuItemList = value; }
        }

        public virtual void AddItem(MenuItem i_InputMenuItem)
        {
            if (this.m_MenuItemList == null)
            {
                this.m_MenuItemList = new List<MenuItem>();
                SubMenuItem backItem = new SubMenuItem("Back");
                backItem.FatherMenuItem = this.FatherMenuItem;
                this.m_MenuItemList.Add(backItem);
            }

            i_InputMenuItem.FatherMenuItem = this;
            this.m_MenuItemList.Add(i_InputMenuItem);
        }

        public void RemoveItem(MenuItem i_InputMenuItem)
        {
            this.m_MenuItemList.Remove(i_InputMenuItem);
        }

        public void ActivateChosenSubMenuItem()
        {
            OnSubMenuItemChosen();
        }

        protected virtual void OnSubMenuItemChosen()
        {
            if (this.SubMenuItemChosen != null)
            {
                this.SubMenuItemChosen.Invoke(this);
            }
        }

        public void Show()
        {
            int optionChosen;

            Console.Clear();
            Console.WriteLine(string.Format("{0}{1}", this.r_MenuItemName, Environment.NewLine));
            for (int i = 1; i < this.m_MenuItemList.Count; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", i, this.m_MenuItemList[i].MenuItemName));
            }

            Console.WriteLine(string.Format("0. {0}", this.m_MenuItemList[0].MenuItemName));
            while (!s_QuitOptionChosen)
            {
                Console.Write(string.Format("{0}Your choice is: ", Environment.NewLine));
                try
                {
                    optionChosen = int.Parse(Console.ReadLine());
                    checkValidMenuOptionInput(optionChosen);
                    if (this.m_MenuItemList[optionChosen] is SubMenuItem)
                    {
                        (this.m_MenuItemList[optionChosen] as SubMenuItem).ActivateChosenSubMenuItem();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine(string.Format("{0}{1}", this.m_MenuItemList[optionChosen].MenuItemName, Environment.NewLine));
                        (this.m_MenuItemList[optionChosen] as ActionMenuItem).ActivateChosenActionMenuItem();
                        Console.WriteLine(string.Format("{0}Press enter to continue...", Environment.NewLine));
                        Console.ReadLine();
                        this.Show();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(string.Format("{0}Your input should be numeric value", Environment.NewLine));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("{0}", ex.Message, Environment.NewLine));
                }
            }
        }

        private void checkValidMenuOptionInput(int i_OptionChosen)
        {
            if (!((i_OptionChosen >= 0) && (i_OptionChosen < this.m_MenuItemList.Count)))
            {
                throw new Exception(string.Format("{0}Please enter a numeric value between 0-{1}", Environment.NewLine, this.m_MenuItemList.Count - 1));
            }
        }
    }
}