using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CTeleportTest.Core.Api;
using CTeleportTest.Core.Contracts;
using CTeleportTest.Core.Services.Interfaces;
using CTeleportTest.Core.Tools;
using Refit;

namespace CTeleportTest.Core.Services.Implementations
{
    public class BookingsService : IBookingsService
    {
        private readonly IConstantsService _constantsService;

        public BookingsService(IConstantsService constantsService)
        {
            _constantsService = constantsService;
        }
        
        private IBookingApi _bookingApi =>
            RestService.For<IBookingApi>(new HttpClient()
            {
                BaseAddress = _constantsService.BaseApiUrl
            });

        public async Task<ServiceResult<List<Booking>>> GetBookings()
        {
            return await _bookingApi.GetBookings().HandleApiCall();
        }
    }
}