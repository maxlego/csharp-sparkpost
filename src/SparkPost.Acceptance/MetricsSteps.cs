using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SparkPost.Acceptance
{
    [Binding]
    public class MetricsSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public MetricsSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [When(@"I query my deliverability for (.*)")]
        public async Task WhenIQueryMyDeliverability(string metric)
        {
            var client = _scenarioContext.Get<IClient>();
            Response response = await client.Metrics.GetDeliverability(new
                {
                    from = DateTime.MinValue,
                    metrics = metric
                });

            _scenarioContext.Set(response);
        }

        [When(@"I query my bounce reasons")]
        public async Task y()
        {
            var client = _scenarioContext.Get<IClient>();
            Response response = await client.Metrics.GetBounceReasons(new
                {
                    from = DateTime.MinValue
                });

            _scenarioContext.Set(response);
        }

        [Then("it should return some metrics count")]
        public void x()
        {
            var response = _scenarioContext.Get<Response>();
            Assert.IsType<GetMetricsResponse>(response);
        }
    }
}