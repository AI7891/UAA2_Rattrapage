using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCoreBusiness.Interfaces.IEmailer
{
    public interface IEmailerService
    {
        public void SendWelcomeEmail(string toEmail, string memberName);
    }
}
