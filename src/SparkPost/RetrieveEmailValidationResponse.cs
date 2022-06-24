using SparkPost.Utilities;

namespace SparkPost
{
    public class RetrieveEmailValidationResponse : Response
    {
        public static EmailValidationResponse CreateFromResponse(Response response)
        {
            var jsonData = Jsonification.DeserializeObject<dynamic>(response.Content).results;
            return EmailValidationResponse.ConvertToResponse(jsonData);
        }
    }
}
