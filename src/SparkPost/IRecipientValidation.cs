using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRecipientValidation
    {
        Task<EmailValidationResponse> Create(string emailAddress);
    }
}
