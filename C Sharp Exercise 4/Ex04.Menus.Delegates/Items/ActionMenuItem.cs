using System;

namespace Ex04.Menus.Delegates.Items
{
    public delegate void ActionMenuItemDelegate();

    public class ActionMenuItem : MenuItem
    {
        public event ActionMenuItemDelegate ActionMenuItemChosen;

        public ActionMenuItem(string i_MenuItemName) : base(i_MenuItemName)
        {
        }

        public void ActivateChosenActionMenuItem()
        {
            OnActionMenuItemChosen();
        }

        protected virtual void OnActionMenuItemChosen()
        {
            if (ActionMenuItemChosen != null)
            {
                ActionMenuItemChosen.Invoke();
            }
            else
            {
                this.QuitOptionChosen = true;
                throw new Exception(string.Format("ERROR: You need to define a Listener to '{0}' action", this.r_MenuItemName));
            }
        }
    }
}