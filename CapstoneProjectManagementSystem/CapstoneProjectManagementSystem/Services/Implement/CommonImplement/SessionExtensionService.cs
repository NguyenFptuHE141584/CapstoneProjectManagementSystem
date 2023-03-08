using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.Implement
{
    public class SessionExtensionService : ISessionExtensionService
    {
        public T GetObjectFromJson<T>(ISession session, string key)
        {
            var value = session.Get(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }

        public void SetObjectAsJson<T>(ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }
    }
}
