using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CTeleportTest.Core.Api;
using Polly;
using Polly.Wrap;
using Refit;

namespace CTeleportTest.Core.Tools
{
    public static class ApiUtils
    {
        public static async Task<ServiceResult<T>> HandleApiCall<T>(this Task<T> task) where T : class, new()
        {
            var serviceResult = new ServiceResult<T>();
            try
            {
                serviceResult.Data = await ApiRetryPolicy().ExecuteAsync(() => task);
            }
            catch (ApiException apiException)
            {
                serviceResult.Exception = apiException;
            }
            catch (Exception exception)
            {
                serviceResult.Exception = exception;
            }
            
            return serviceResult;
        }
        
        private static List<HttpStatusCode> _httpStatusCodesWorthRetrying = new List<HttpStatusCode>
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout, // 504
        };

        private static AsyncPolicyWrap ApiRetryPolicy()
        {
            var wireServerNetworkIssue = Policy.Handle<WebException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4)
                });
            var api = Policy
                .Handle<ApiException>(ex => _httpStatusCodesWorthRetrying.Contains(ex.StatusCode))
                .RetryAsync(1, onRetry: async (exception, i) =>
                {
                    var apiException = exception as ApiException;

                    if (apiException != null && apiException.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Console.WriteLine("Unauthorized API exception: " + apiException);
                        //TODO: Handle unauthorized requests
                    }
                });
            
            return Policy.WrapAsync(wireServerNetworkIssue, api);
        }
    }
}