using System;
using CTeleportTest.Core.Services.Interfaces;

namespace CTeleportTest.Core.Services.Implementations
{
    public class ConstantsService : IConstantsService
    {
        public Uri BaseApiUrl => new Uri("https://cteleport-dev.s3.eu-central-1.amazonaws.com");
    }
}