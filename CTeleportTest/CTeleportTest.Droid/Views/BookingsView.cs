using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using CTeleportTest.Core.ViewModels.Bookings;
using CTeleportTest.Droid.TemplateSelectors;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace CTeleportTest.Droid.Views
{
    [Activity(Label = "Bookings")]
    public class BookingsView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.BookingsView;
        private MvxRecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            _recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.recycler_view);
            if (_recyclerView != null)
            {
                _recyclerView.HasFixedSize = false;
                _layoutManager = new LinearLayoutManager(this);
                _recyclerView.SetLayoutManager(_layoutManager);
                _recyclerView.ItemTemplateSelector = new TypeTemplateSelector(
                    new Dictionary<Type, int>
                    {
                        {typeof(BookingItem), Resource.Layout.item_booking},
                        {typeof(BookingGroup), Resource.Layout.item_booking_group}
                    });
            }
        }
    }
}