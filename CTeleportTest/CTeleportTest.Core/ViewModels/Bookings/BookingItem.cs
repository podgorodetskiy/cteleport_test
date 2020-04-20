using System;
using System.Collections.Generic;
using System.Linq;
using CTeleportTest.Core.Contracts;

namespace CTeleportTest.Core.ViewModels.Bookings
{
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
                
            Terms = String.Join(", ", termsList);
        }
    }
}