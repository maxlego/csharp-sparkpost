using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class MessageEventsSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public MessageEventsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I ask for samples of '(.*)'")]
        public async Task WhenIAskForSamplesOf(string events)
        {
            var client = _scenarioContext.Get<IClient>();

            MessageEventSampleResponse response = await client.MessageEvents.SamplesOf(events);

            _scenarioContext.Set(response);
            _scenarioContext.Set<Response>(response);
        }
    }
}
