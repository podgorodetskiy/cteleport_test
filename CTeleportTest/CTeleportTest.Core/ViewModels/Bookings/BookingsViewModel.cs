using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CTeleportTest.Core.Api;
using CTeleportTest.Core.Contracts;
using CTeleportTest.Core.Services.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Refit;

namespace CTeleportTest.Core.ViewModels
{
    public class BookingsViewModel : MvxViewModel
    {
        private readonly IBookingsService _bookingsService;
        
        private MvxObservableCollection<object> _items = new MvxObservableCollection<object>();
        private bool _isBookingsEmpty = true;
        private bool _showError;
        private string _errorMessage;
        private bool _isLoading;
        private MvxNotifyTask _loadBookingsTask;

        public BookingsViewModel(IBookingsService bookingsService)
        {
            _bookingsService = bookingsService;
        }
        
        public bool IsBookingsEmpty
        {
            get => _isBookingsEmpty;
            set { this.RaiseAndSetIfChanged(ref _isBookingsEmpty, value, () => IsBookingsEmpty); }
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
                    ? "Network error" : "Something went wrong";
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

            var result = bookingsResult.Data.GroupBy(x => new
            {
                x.Metadata.VesselName,
                x.Metadata.CrewChangeAirport,
                x.Metadata.CrewChangeDate
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
            Items.AddRange(result);

            IsLoading = false;
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

        public class BookingGroup
        {
            private readonly string _vesselName;
            private readonly DateTimeOffset? _crewChangeDate;

            public BookingGroup(string vesselName, string crewChangeAirport, DateTimeOffset? crewChangeDate)
            {
                _vesselName = vesselName;
                _crewChangeDate = crewChangeDate;
                CrewChangeAirport = crewChangeAirport;
            }
            
            public string GroupTitle => _vesselName ?? "OFFICE STAFF";
            
            public string CrewChangeAirport { get; set; }
            
            public string CrewChangeDateFormatted => _crewChangeDate?.ToString("d/M");
        }

        public class BookingItem
        {
            private readonly Booking _booking;

            public BookingItem(Booking booking)
            {
                _booking = booking;
                GenerateFields();
            }

            public string Name { get; private set; }
            public string Origin { get; private set; }
            public string Destination { get; private set; }
            public string Terms { get; private set; }
            public bool BothWays { get; private set; }
            
            private void GenerateFields()
            {
                Name = _booking.PaxName;
                
                if (_booking.Legs != null && _booking.Legs.Any())
                {
                    BothWays = _booking.Legs.Count > 1;
                    var firstLeg = _booking.Legs.OrderBy(x => x.Departure).FirstOrDefault();
                    Origin = firstLeg?.Origin;
                    Destination = firstLeg?.Destination;
                }
                
                var termsList = new List<Enum>
                {
                    _booking.State,
                    _booking.Terms?.FareType,
                    _booking.Terms?.Conditions
                }
                .Where(x=> x != null)
                .Select(y => y.ToString());
                
                Terms = string.Join(", ", termsList);
            }
        }
    }
}
