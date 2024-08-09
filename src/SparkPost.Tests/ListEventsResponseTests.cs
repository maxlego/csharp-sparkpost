using Xunit;

namespace SparkPost.Tests
{
    public class ListEventsResponseTests
    {
        public class DefaultTests
        {
            [Fact]
            public void It_should_not_have_nil_links()
            {
                var response = new ListEventsResponse();
                Assert.NotNull(response.Link);
            }

            [Fact]
            public void It_should_not_have_nil_events()
            {
                var response = new ListEventsResponse();
                Assert.NotNull(response.Events);
            }
        }
    }
}
