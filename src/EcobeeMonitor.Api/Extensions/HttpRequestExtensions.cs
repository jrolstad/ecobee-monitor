using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Threading.Tasks;

namespace EcobeeMonitor.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Task<HttpResponseData> OkResponseAsync(this HttpRequestData request)
        {
            return Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
        }

        public static async Task<HttpResponseData> OkResponseAsync<T>(this HttpRequestData request, T data)
        {
            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(data);

            return response;
        }
    }
}
