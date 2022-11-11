namespace _0_Framework.Application.Email
{
    public interface IEmailService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param> Email title
        /// <param name="messageBody"></param>messageBody Email
        /// <param name="destination"></param> destination Emal
        void SendEmail(string title, string messageBody, string destination);
    }
}