using System.Net;
using System.Threading.Tasks;
using SparkPost.RequestSenders;

namespace SparkPost
{
    public class RecipientValidation : IRecipientValidation
    {
        private readonly IClient client;
        private readonly IRequestSender requestSender;

        public RecipientValidation(IClient client, IRequestSender requestSender)
        {
            this.client = client;
            this.requestSender = requestSender;
        }

        public async Task<EmailValidationResponse> Create(string emailAddress)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/recipient-validation/single/{emailAddress}",
                Method = "GET"
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ResponseException(response);
            }

            return RetrieveEmailValidationResponse.CreateFromResponse(response);
        }
    }
}
