using System;
using TechTalk.SpecFlow;

namespace SparkPost.Acceptance
{
    [Obsolete]
    [Binding]
    public class TransmissionsSteps
    {
        [Given(@"I have a new transmission")]
        public void GivenIHaveANewTransmission()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission is meant to be sent from '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentFrom(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission is meant to be sent to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeSentTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission content is")]
        public void GivenTheTransmissionContentIs(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission has a text file attachment")]
        public void GivenTheTransmissionHasATextFileAttachment()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission template id is set to '(.*)'")]
        public void GivenTheTransmissionTemplateIdIsSetTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission is meant to be CCd to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeCCdTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"the transmission is meant to be BCCd to '(.*)'")]
        public void GivenTheTransmissionIsMeantToBeBCCdTo(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I send the transmission")]
        public void WhenISendTheTransmission()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
