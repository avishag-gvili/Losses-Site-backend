using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface ISendEmailService
    {
        void SendEmail(string to, string name, string htmlBody, string Subject);
    }
}
