using System.Net;
using TechnicalTestDotNet.Core.Models;

namespace TechnicalTestDotNet.Core.Helpers.CustomHttpExceptions
{
    public class CustomHttpException : Exception
    {
        public List<string> Messages { get; set; }
        public string MessageUser { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string PatchError { get; set; }

        public CustomHttpException()
        {
            Messages = new List<string>();
        }

        public CustomHttpException(ErrorDetails errorDetails)
        {
            StatusCode = (HttpStatusCode)errorDetails.StatusCode;
            Messages = errorDetails.Messages;
            MessageUser = errorDetails.MessageUser;
            PatchError = errorDetails.PatchError;
        }
    }
}
