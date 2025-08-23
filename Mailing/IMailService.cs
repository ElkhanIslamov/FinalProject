using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailing
{
    public interface IMailService
    {
        void SendMail(Mail mail);
    }
}
