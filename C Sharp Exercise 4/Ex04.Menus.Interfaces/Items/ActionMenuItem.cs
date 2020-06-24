using System;
using Ex04.Menus.Interfaces.IListeners;

namespace Ex04.Menus.Interfaces.Items
{
    public sealed class ActionMenuItem : MenuItem
    {
        private readonly int r_ActionID;
        private IActionsListener m_ActionsListener;

        public ActionMenuItem(string i_MenuItemName, int i_ActionID) : base(i_MenuItemName)
        {
            this.r_ActionID = i_ActionID;
        }

        public IActionsListener ActionsListener
        {
            get
            {
                if (this.m_ActionsListener != null)
                {
                    return m_ActionsListener;
                }
                else
                {
                    this.QuitOptionChosen = true;
                    throw new Exception(string.Format("ERROR: You need to define a Listener to '{0}' action", this.r_MenuItemName));
                }
            }

            set { m_ActionsListener = value; }
        }

        public int ActionID
        {
            get { return this.r_ActionID; }
        }
    }
}