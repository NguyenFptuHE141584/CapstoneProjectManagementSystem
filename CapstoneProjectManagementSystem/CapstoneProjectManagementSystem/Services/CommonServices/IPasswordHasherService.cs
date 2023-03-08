using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool PasswordVerificationResult (string hashedPassword,string password);
    }
}
