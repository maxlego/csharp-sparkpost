using System;
using System.Threading.Tasks;

namespace SparkPost
{
    [Obsolete("Deprecated in 2019")]
    public interface IMessageEvents
    {
        Task<ListMessageEventsResponse> List();
        Task<ListMessageEventsResponse> List(object query);
        Task<MessageEventSampleResponse> SamplesOf(string events);
    }
}
