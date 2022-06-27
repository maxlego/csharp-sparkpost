using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SparkPost.Acceptance
{
    [Binding]
    public class SuppressionsSteps
    {
        private readonly ScenarioContext scenarioContext;

        public SuppressionsSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I have a random email address ending in '(.*)'")]
        public void y(string email)
        {
            scenarioContext["randomemail"] = $"{Guid.NewGuid().ToString().Split('-')[0]}{email}";
        }

        [When(@"I add my random email address a to my suppressions list")]
        public async Task WhenIAddToMySuppressionsList()
        {
            var email = scenarioContext["randomemail"] as string;

            var client = scenarioContext.Get<IClient>();

            UpdateSuppressionResponse response =  await client.Suppressions.CreateOrUpdate(new [] {email});

            scenarioContext.Set(response);
            scenarioContext.Set<Response>(response);
        }

        [Then(@"my random email address should be on my suppressions list")]
        public async Task ThenShouldBeOnMySuppressionsList()
        {
            var email = scenarioContext["randomemail"] as string;

            var client = scenarioContext.Get<IClient>();

            ListSuppressionResponse response = null;

            response = await client.Suppressions.Retrieve(email);
            Assert.True(response.Suppressions.Any());

        }
    }
}