using System;

namespace CTeleportTest.Core.ViewModels.Bookings
{
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
}