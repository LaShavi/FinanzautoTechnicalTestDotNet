using System.Net;
using System.Text.Json;

namespace TechnicalTestDotNet.Core.Models
{
    public class ErrorDetails
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Messages { get; set; }
        public string MessageUser { get; set; }
        public string PatchError { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
