using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SparkPost.RequestSenders;
using SparkPost.Utilities;

namespace SparkPost
{
    public class Events : IEvents
    {
        private readonly Client client;
        private readonly IRequestSender requestSender;

        public Events(Client client, IRequestSender requestSender)
        {
            this.client = client;
            this.requestSender = requestSender;
        }

        public async Task<ListEventsResponse> List()
        {
            return await List((EventsQuery)null);
        }

        public async Task<ListEventsResponse> List(EventsQuery eventsQuery)
        {
            return await this.List($"/api/{client.Version}/events/message", eventsQuery);
        }

        public async Task<ListEventsResponse> List(string url)
        {
            return await this.List(url, null);
        }

        public async Task<ListEventsResponse> List(string url, EventsQuery eventsQuery)
        {
            var request = new Request
            {
                Url = url,
                Method = "GET",
                Data = (object)eventsQuery ?? new { }
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ResponseException(response);

            dynamic content = Jsonification.DeserializeObject<dynamic>(response.Content);

            var listMessageEventsResponse = new ListEventsResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Events = ConvertResultsToAListOfEvents(content.results),
                TotalCount = content.total_count,
                Link = ConvertToLinks(content.links)
            };

            return listMessageEventsResponse;
        }

        public async Task<EventSampleResponse> SamplesOf(string events)
        {
            var request = new Request { Url = $"/api/{client.Version}/events/message/samples?events={events}", Method = "GET" };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ResponseException(response);

            return new EventSampleResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
            };
        }

        private static EventPageLink ConvertToLinks(dynamic page_links)
        {
            var links = new EventPageLink();

            if (page_links != null)
                links.Next = page_links.next;

            return links;
        }

        private static IEnumerable<Event> ConvertResultsToAListOfEvents(dynamic results)
        {
            var events = new List<Event>();

            if (results == null)
                return events;

            foreach (var result in results)
            {
                var metadata = Jsonification.DeserializeObject<Dictionary<string, string>>(Jsonification.SerializeObject(result.rcpt_meta));
                var tags = Jsonification.DeserializeObject<List<string>>(Jsonification.SerializeObject(result.rcpt_tags));

                events.Add(
                    new Event
                    {
                        Type = result.type,
                        BounceClass = result.bounce_class,
                        CampaignId = result.campaign_id,
                        CustomerId = result.customer_id,
                        DeliveryMethod = result.delv_method,
                        DeviceToken = result.device_token,
                        ErrorCode = result.error_code,
                        IpAddress = result.ip_address,
                        MessageId = result.message_id,
                        MessageFrom = result.msg_from,
                        MessageSize = result.msg_size,
                        NumberOfRetries = result.num_retries,
                        RecipientTo = result.rcpt_to,
                        RecipientType = result.rcpt_type,
                        RawReason = result.raw_reason,
                        Reason = result.reason,
                        RoutingDomain = result.routing_domain,
                        Subject = result.subject,
                        TemplateId = result.template_id,
                        TemplateVersion = result.template_version,
                        Timestamp = result.timestamp,
                        TransmissionId = result.transmission_id,
                        EventId = result.event_id,
                        FriendlyFrom = result.friendly_from,
                        IpPool = result.ip_pool,
                        QueueTime = result.queue_time,
                        RawRecipientTo = result.raw_rcpt_to,
                        SendingIp = result.sending_ip,
                        Transactional = result.transactional,
                        RemoteAddress = result.remote_addr,
                        Metadata = metadata,
                        TargetLinkUrl = result.target_link_url,
                        Tags = tags
                    }
                );
            }
            return events;
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
