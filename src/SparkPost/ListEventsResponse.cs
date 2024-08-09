using System.Collections.Generic;

namespace SparkPost
{
    public class ListEventsResponse : Response
    {
        public ListEventsResponse()
        {
            Events = new Event[] { };
            Link = new EventPageLink();
        }

        public IEnumerable<Event> Events { get; set; }

        public EventPageLink Link { get; set; }

        public int TotalCount { get; set; }
    }
}
