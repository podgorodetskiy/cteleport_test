using System;

namespace CTeleportTest.Core.Api
{
    public class ServiceResult<T> where T : class
    {
        public T Data { get; set; }
        public Exception Exception { get; set; }
    }
}