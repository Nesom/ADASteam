using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace EmailNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            // вызов программы
            EmailNotification.GetEmailNotification();
        }
    }
}
