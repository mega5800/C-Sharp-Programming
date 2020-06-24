namespace Ex04.Menus.Interfaces.Items
{
    public class MenuItem
    {
        protected static bool s_QuitOptionChosen;
        protected readonly string r_MenuItemName;
        protected MenuItem m_FatherMenuItem;

        internal MenuItem(string i_MenuItemName)
        {
            this.r_MenuItemName = i_MenuItemName;
        }

        public string MenuItemName
        {
            get { return this.r_MenuItemName; }
        }

        public MenuItem FatherMenuItem
        {
            get { return this.m_FatherMenuItem; }
            set { this.m_FatherMenuItem = value; }
        }

        public bool QuitOptionChosen
        {
            get { return s_QuitOptionChosen; }
            set { s_QuitOptionChosen = value; }
        }
    }
}