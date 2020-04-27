using System;
using Android.Views;

namespace CTeleportTest.Droid.Utils
{
    public class MenuItemClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
    {
        private readonly Action _clickAction;

        public MenuItemClickListener(Action clickAction)
        {
            _clickAction = clickAction;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            _clickAction?.Invoke();
            return true;
        }
    }
}