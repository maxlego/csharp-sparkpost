using Xunit;

namespace SparkPost.Tests
{
    public class EventsQueryTests
    {
        public class Defaults
        {
            [Fact]
            public void It_should_have_a_default_build_list()
            {
                Assert.NotNull(new EventsQuery().BounceClasses);
            }

            [Fact]
            public void It_should_have_a_default_campaigns_list()
            {
                Assert.NotNull(new EventsQuery().Campaigns);
            }

            [Fact]
            public void It_should_have_a_default_from_addresses_list()
            {
                Assert.NotNull(new EventsQuery().FromAddresses);
            }

            [Fact]
            public void It_should_have_a_default_messages_list()
            {
                Assert.NotNull(new EventsQuery().Messages);
            }

            [Fact]
            public void It_should_have_a_recipients_list()
            {
                Assert.NotNull(new EventsQuery().Recipients);
            }

            [Fact]
            public void It_should_have_a_Subaccounts_list()
            {
                Assert.NotNull(new EventsQuery().Subaccounts);
            }

            [Fact]
            public void It_should_have_templates_list()
            {
                Assert.NotNull(new EventsQuery().Templates);
            }

            [Fact]
            public void It_should_have_transmissions_list()
            {
                Assert.NotNull(new EventsQuery().Transmissions);
            }
        }
    }
}
