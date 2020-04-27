using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CTeleportTest.Core.Contracts;
using CTeleportTest.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Refit;

namespace CTeleportTest.Core.ViewModels.Bookings
{
    public class BookingsViewModel : MvxViewModel
    {
        private static readonly SortedDictionary<string, Func<Booking, object>> SortFields
            = new SortedDictionary<string, Func<Booking, object>>
            {
                {"Departure date", booking => booking.DepartureAt},
                {"Booking date", booking => booking.CreatedAt}
            };
        
        private readonly IMvxNavigationService _navigationService;
        private readonly IBookingsService _bookingsService;

        private MvxObservableCollection<object> _items = new MvxObservableCollection<object>();
        private bool _showError;
        private string _errorMessage;
        private bool _isLoading;
        private string _searchString;
        private MvxNotifyTask _loadBookingsTask;
        private KeyValuePair<string, Func<Booking, object>> _selectedSortField = SortFields.FirstOrDefault();

        private List<Booking> _originalBookingsList;

        public BookingsViewModel(
            IMvxNavigationService navigationService,
            IBookingsService bookingsService)
        {
            _navigationService = navigationService;
            _bookingsService = bookingsService;
        }

        public bool ShowError
        {
            get => _showError;
            set { this.RaiseAndSetIfChanged(ref _showError, value, () => ShowError); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { this.RaiseAndSetIfChanged(ref _errorMessage, value, () => ErrorMessage); }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { this.RaiseAndSetIfChanged(ref _isLoading, value, () => IsLoading); }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                if (value == _searchString)
                    return;
                this.RaiseAndSetIfChanged(ref _searchString, value, () => SearchString);
                ProcessBookings();
            }
        }

        public MvxObservableCollection<object> Items
        {
            get => _items;
            set { this.RaiseAndSetIfChanged(ref _items, value, () => Items); }
        }

        public MvxCommand ReloadCommand => new MvxCommand(ResetLoadBookingsTask);

        public override void Start()
        {
            base.Start();
            CreateLoadBookingsTask();
        }

        public async void ShowSortSelectionModel()
        {
            var sortFieldName =
                await _navigationService.Navigate<StringSelectionViewModel, StringSelectionViewModel.InitParams, string>(
                    new StringSelectionViewModel.InitParams("Sort by", _selectedSortField.Key, SortFields.Keys.ToList()));
            _selectedSortField = SortFields.FirstOrDefault(x => x.Key == sortFieldName);
            ProcessBookings();
        }

        private async Task LoadBookings()
        {
            IsLoading = true;

            var bookingsResult = await _bookingsService.GetBookings();
            if (bookingsResult == null)
            {
                ShowError = true;
                ErrorMessage = "Something went wrong";
                IsLoading = false;
                return;
            }

            if (bookingsResult.Exception != null)
            {
                ShowError = true;
                ErrorMessage = bookingsResult.Exception is ApiException
                               || bookingsResult.Exception is HttpRequestException
                    ? "Network error"
                    : "Something went wrong";
                IsLoading = false;
                return;
            }

            if (bookingsResult.Data == null || !bookingsResult.Data.Any())
            {
                ShowError = true;
                ErrorMessage = "List of bookings is empty";
                IsLoading = false;
                return;
            }

            ShowError = false;

            _originalBookingsList = bookingsResult.Data;
            ProcessBookings();

            IsLoading = false;
        }

        private void ProcessBookings()
        {
            if (_originalBookingsList == null || !_originalBookingsList.Any())
                return;
            var flattenedGroupedItems = _originalBookingsList
                .OrderBy(b => _selectedSortField.Value(b))
                .Where(b =>
                    (!b.NoShow ?? true)
                    &&
                    (string.IsNullOrEmpty(SearchString)
                     || (b.PaxName?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ?? false)
                     || (b.Metadata?.VesselName?.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ?? false)))
                .GroupBy(x => new
                {
                    x.Metadata.VesselName,
                    x.Metadata.CrewChangeAirport,
                    CrewChangeDate = x.Metadata.CrewChangeDate ?? x.DepartureAt
                }).SelectMany(g =>
                {
                    var flattenedList = new List<object>
                    {
                        new BookingGroup(g.Key.VesselName, g.Key.CrewChangeAirport, g.Key.CrewChangeDate)
                    };
                    flattenedList.AddRange(g.Select(b => new BookingItem(b)));
                    return flattenedList;
                }).ToList();
            Items.Clear();
            Items.AddRange(flattenedGroupedItems);
        }

        private void CreateLoadBookingsTask()
        {
            _loadBookingsTask = MvxNotifyTask.Create(LoadBookings);
        }

        private void ResetLoadBookingsTask()
        {
            ShowError = false;
            CreateLoadBookingsTask();
        }
    }
}