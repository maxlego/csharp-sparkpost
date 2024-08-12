using System;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IEvents
    {
        Task<ListEventsResponse> List();
        Task<ListEventsResponse> List(EventsQuery query);
        Task<ListEventsResponse> List(string url);
        Task<EventSampleResponse> SamplesOf(string events);
        Task<JsonEventResponse> Get(string cursor = "initial", int perPage = 1000);
        Task<JsonEventResponse> GetEventsSince(DateTime fromTime, int perPage = 1000);
        Task<JsonEventResponse> GetEventsNext(string nextUri);
    }
}
