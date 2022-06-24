using TechTalk.SpecFlow;
using Xunit;

namespace SparkPost.Acceptance
{
    [Binding]
    public class ResponseSteps
    {
        [Then(@"it should return a (.*)")]
        public void ThenItShouldReturnA(int statusCode)
        {
            var response = ScenarioContext.Current.Get<Response>();
             Assert.Equal(statusCode, response.StatusCode.GetHashCode());
        }
    }
}