using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPost.RequestSenders
{
    public class AsyncRequestSender : IRequestSender
    {
        private readonly IClient client;
        private readonly IDataMapper dataMapper;
        private readonly Func<HttpClient> httpClientFactory;

        public AsyncRequestSender(IClient client, IDataMapper dataMapper, Func<HttpClient> httpClientFactory)
        {
            this.client = client;
            this.dataMapper = dataMapper;
            this.httpClientFactory = httpClientFactory;
        }

        public virtual async Task<Response> Send(Request request)
        {
                var httpClient = httpClientFactory();
                PrepareTheHttpClient(httpClient, client);

                var result = await GetTheResponse(request, httpClient);

                return new Response
                {
                    StatusCode = result.StatusCode,
                    ReasonPhrase = result.ReasonPhrase,
                    Content = await result.Content.ReadAsStringAsync()
                };
        }

        private static void PrepareTheHttpClient(HttpClient httpClient, IClient client)
        {
            httpClient.BaseAddress = new Uri(client.ApiHost);
            httpClient.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

            if (client.SubaccountId != 0)
                httpClient.DefaultRequestHeaders.Add("X-MSYS-SUBACCOUNT",
                    client.SubaccountId.ToString(CultureInfo.InvariantCulture));
        }

        protected virtual async Task<HttpResponseMessage> GetTheResponse(Request request, HttpClient httpClient)
        {
            return await new RequestMethodFinder(httpClient, dataMapper)
                .FindFor(request)
                .Execute(request);
        }
    }
}