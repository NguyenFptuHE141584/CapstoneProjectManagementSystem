using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services
{
    public interface ISessionExtensionService
    {
        public T GetObjectFromJson<T>(ISession session, string key);
        public void SetObjectAsJson<T>(ISession session, string key, T value);
       
    }
}
