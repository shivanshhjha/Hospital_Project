using System;
using System.Text.Json;
namespace HospitalApp.CustomSession
{
    public static class AppSessionExtension
    {
        /// <summary>
        /// Set the Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            // The CLR Object will be Serialized in the form
            // of JSON and will be stored by the server in Session
            // Storage
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        /// <summary>
        /// Get Object from Session State
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObject<T>(this ISession session, string key)
        {
            // First Get the String Data
            string data = session.GetString(key);
            if (data == null)
                // return the default Empty Instance of the CLR object
                return default(T);
            // deserilze the Data from the Session and return it
            return JsonSerializer.Deserialize<T>(data);
        }

    }
}

