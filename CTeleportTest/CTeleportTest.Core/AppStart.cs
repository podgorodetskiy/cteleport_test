﻿using System.Threading.Tasks;
using CTeleportTest.Core.ViewModels.Bookings;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace CTeleportTest.Core
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app,
            IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<BookingsViewModel>();
        }
    }
}
