using TechTalk.SpecFlow;
using Xunit;

namespace SparkPost.Acceptance
{
    [Binding]
    public class ResponseSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public ResponseSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"it should return a (.*)")]
        public void ThenItShouldReturnA(int statusCode)
        {
            var response = _scenarioContext.Get<Response>();
            Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }
    }
}
