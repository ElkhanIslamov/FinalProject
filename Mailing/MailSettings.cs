using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing
{
    public class MailSettings
    {
        public required string Server { get; set; }
        public int Port { get; set; }
        public required string SenderFullName { get; set; }
        public required string SenderEmail { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? HtmlBody { get; set; }

        public MailSettings()
        {
        }

        public MailSettings(string server, int port, string senderFullName, string senderEmail, string userName, string password, string? htmlBody)
        {
            Server = server;
            Port = port;
            SenderFullName = senderFullName;
            SenderEmail = senderEmail;
            UserName = userName;
            Password = password;
            HtmlBody = htmlBody;
        }

    }
}
