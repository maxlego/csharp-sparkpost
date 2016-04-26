using System;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRequestSender
    {
        Task<Response> Send(Request request);
    }

    public class RequestSender : IRequestSender
    {
        private readonly Client client;

        public RequestSender(Client client)
        {
            this.client = client;
        }

        public async Task<Response> Send(Request request)
        {
            using (var httpClient = client.CustomSettings.CreateANewHttpClient())
            {
                if (request.Url.StartsWith("api") == false)
                    request.Url = $"api/{client.Version}/{request.Url}";

                httpClient.BaseAddress = new Uri(client.ApiHost);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Authorization", client.ApiKey);

                var result = await new RequestMethodFinder(httpClient)
                    .FindFor(request)
                    .Execute(request);

                return new Response
                {
                    StatusCode = result.StatusCode,
                    ReasonPhrase = result.ReasonPhrase,
                    Content = await result.Content.ReadAsStringAsync()
                };
            }
        }
    }
}