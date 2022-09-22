using System;
namespace HospitalApp.Models
{
    /// <summary>
    /// A Common Reponnse Status class
    /// This will be defing public Properties
    /// for Sending Response to Client
    /// Client will be a Razor View or could be a JavaScript Client App
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseStatus<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<T> Records { get; set; } = null;
        public T Record { get; set; }
    }
}

