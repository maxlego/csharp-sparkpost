using System.Threading.Tasks;

namespace SparkPost.RequestSenders
{
    public class SyncRequestSender : IRequestSender
    {
        private readonly IRequestSender requestSender;

        public SyncRequestSender(IRequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        public virtual async Task<Response> Send(Request request)
        {
            Response response = await requestSender.Send(request);

            return response;
        }
    }
}