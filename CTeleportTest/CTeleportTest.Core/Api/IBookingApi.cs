using System.Collections.Generic;
using System.Threading.Tasks;
using CTeleportTest.Core.Contracts;
using Refit;

namespace CTeleportTest.Core.Api
{
    public interface IBookingApi
    {
        [Get("/bookings-payload/bookings.json")]
        Task<List<Booking>> GetBookings();
    }
}