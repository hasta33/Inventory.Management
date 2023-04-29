using Email.API.Dtos;

namespace Email.API.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
