using System.Collections.Generic;
using System.Threading.Tasks;
using CTeleportTest.Core.Api;
using CTeleportTest.Core.Contracts;

namespace CTeleportTest.Core.Services.Interfaces
{
    public interface IBookingsService
    {
        Task<ServiceResult<List<Booking>>> GetBookings();
    }
}