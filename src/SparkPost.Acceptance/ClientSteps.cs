using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Binding]
    public class ClientSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ClientSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [Given(@"my api key is '(.*)'")]
        public void GivenMyApiKeyIs(string apiKey)
        {
            var client = new Client(apiKey);
            _scenarioContext.Set<IClient>(client);
        }
    }
}