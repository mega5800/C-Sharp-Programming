using Ex04.Menus.Interfaces.Items;

namespace Ex04.Menus.Interfaces.IListeners
{
    internal interface ISelectedMenuItemListener
    {
        void PerformSubMenuItemSelection(SubMenuItem i_SubMenuItem);
    }
}