using System;
using System.Net;
using System.Net.Mail;

namespace EmailNotification
{
    public class EmailNotification
    {
        public static void GetEmailNotification()
        {
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("adasproject4@gmail.com", "ADAS");
            // кому отправляем
            MailAddress to = new MailAddress("dimono30042000@mail.ru");
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Новые скидки";
            // текст письма
            m.Body = "<h2>Купите айфон по цене двух!</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("adasproject4@gmail.com", "adasproject4!!");
            smtp.EnableSsl = true;
            // отправляем пиьсмо
            smtp.Send(m);
            Console.Read();
        }
    }
}
