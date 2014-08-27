namespace StarScream.Domain.Email
{
    public interface ISmtpClient
    {
        void Send(string emailAddress, string subject, string body);
    }
}