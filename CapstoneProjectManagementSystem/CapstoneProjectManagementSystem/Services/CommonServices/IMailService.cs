﻿using CapstoneProjectManagementSystem.Models.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IMailService
    {
        Task SendMail(MailContent mailContent);
        Task SendEmailAsync(string email, string subject, string token);
        string GetMailBody(string personalEmail, string token);
        string GenerateOTP();
        Task SendMailChangeRequest(string receiver, string cc, string subject, string body);
        Task SendMailNotification(string receiver, string cc, string subject, string body);
    }
}
