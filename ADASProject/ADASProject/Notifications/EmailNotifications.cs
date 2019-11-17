using ADASProject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ADASProject.Notifications
{
    public class EmailNotification : INotificationService
    {
        // Status to (theme, text) dictionary
        private static Dictionary<Status, Tuple<string, string>> orderMails { get; }

        static EmailNotification()
        {
            orderMails = new Dictionary<Status, Tuple<string, string>>();

            orderMails.Add(Status.Review, Tuple.Create("Order review", "<h3>Your order is in review now.<h3>"));
            orderMails.Add(Status.Sent, Tuple.Create("Order sent", "<h3>Your order has been sent.<h3>"));
            orderMails.Add(Status.Delivered, Tuple.Create("Order delivered", "<h3>Your order has been delivered.<h3>"));
            orderMails.Add(Status.Received, Tuple.Create("Order received", "<h3>You have confirmed receipt of the order.<h3>"));
        }

        public static async Task SendMailAsync(string email, string subject, string message)
        {
            MailAddress from = new MailAddress("adasproject4@gmail.com", "ADAS");
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subject;
            // текст письма
            m.Body = message;
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("adasproject4@gmail.com", "adasproject4!!");
            smtp.EnableSsl = true;
            // отправляем пиьсмо
            await smtp.SendMailAsync(m);
        }

        public async Task SendOrderNotificationAsync(string email, OrderInfo order, Status type)
        {
            var body = $"<h3>Information about order ({order.Id}).<h3>\n" + orderMails[type].Item2 + "\n<h4>Thank you for using ADAS shop!<h4>";
            await SendMailAsync(email, orderMails[type].Item1, body);
        }

        public Task SendRegisterNotificationAsync(string email, string password, RegisterMailType type)
        {
            throw new NotImplementedException();
        }
    }
}
