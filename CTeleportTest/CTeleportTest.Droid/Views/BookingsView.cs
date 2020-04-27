using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using CTeleportTest.Core.ViewModels.Bookings;
using CTeleportTest.Droid.TemplateSelectors;
using CTeleportTest.Droid.Utils;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace CTeleportTest.Droid.Views
{
    [Activity(Label = "Bookings")]
    public class BookingsView : BaseView<BookingsViewModel>, SearchView.IOnQueryTextListener
    {
        protected override int LayoutResource => Resource.Layout.BookingsView;
        private MvxRecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        private SearchView _searchView;
        private IMenuItem _searchMenuItem;
        private IMenuItem _sortMenuItem;
        
        private bool _showError;

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

            var set = this.CreateBindingSet<BookingsView, BookingsViewModel>();
            set.Bind(this).For(v => v.ShowError).To(vm => vm.ShowError);
            set.Apply();
        }

        public bool ShowError
        {
            get => _showError;
            set
            {
                _showError = value;
                UpdateMenuItemsVisibility();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.bookings_menu, menu);
            _sortMenuItem = menu.FindItem(Resource.Id.action_sort);
            _sortMenuItem.SetOnMenuItemClickListener(new MenuItemClickListener(ViewModel.ShowSortSelectionModel));
            _searchMenuItem = menu.FindItem(Resource.Id.action_search);
            UpdateMenuItemsVisibility();
            _searchView = (SearchView) _searchMenuItem.ActionView;
            _searchView.SetOnQueryTextListener(this);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_sort:
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        
        private void UpdateMenuItemsVisibility()
        {
            _searchMenuItem?.SetVisible(!_showError);
            _sortMenuItem?.SetVisible(!_showError);
        }

        public bool OnQueryTextChange(string newText)
        {
            ViewModel.SearchString = newText;
            return false;
        }

        public bool OnQueryTextSubmit(string newText)
        {
            return false;
        }
    }
}