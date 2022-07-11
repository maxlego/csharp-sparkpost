using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SparkPost.RequestSenders;
using SparkPost.Utilities;

namespace SparkPost
{
    public class Events
    {
        private readonly Client client;
        private readonly IRequestSender requestSender;

        public Events(Client client, IRequestSender requestSender)
        {
            this.client = client;
            this.requestSender = requestSender;
        }

        public async Task<JsonEventResponse> Get(string cursor = "initial", int perPage = 1000)
        {
            var request = new Request { Url = $"api/{client.Version}/events/message?perPage={perPage}&cursor={cursor}", Method = "GET" };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ResponseException(response);
            }
            var results = Jsonification.DeserializeObject<JsonEventResponse>(response.Content);
            return results;
        }

        public async Task<JsonEventResponse> GetEventsSince(DateTime fromTime, int perPage = 1000)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/events/message?perPage={perPage}&from={fromTime:yyyy-MM-ddTHH:mm:ssZ}",
                Method = "GET"
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ResponseException(response);
            }
            var results = Jsonification.DeserializeObject<JsonEventResponse>(response.Content);
            return results;
        }

        public async Task<JsonEventResponse> GetEventsNext(string nextUri)
        {
            var request = new Request { Url = nextUri, Method = "GET" };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ResponseException(response);
            }
            var results = Jsonification.DeserializeObject<JsonEventResponse>(response.Content);
            return results;
        }
    }

    public class JsonEventResponse
    {
        [JsonProperty("results")]
        public JObject[] Results { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public string Next { get; set; }
    }
}
