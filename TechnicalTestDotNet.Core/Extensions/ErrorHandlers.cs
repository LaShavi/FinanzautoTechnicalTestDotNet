using System.Net;
using TechnicalTestDotNet.Core.Models;

namespace TechnicalTestDotNet.Core.Extensions
{
    public static class ErrorHandlers
    {
        private static readonly ErrorDetails _errorDetails = new();

        public static void ObjectIfIsNotNull(this object obj, string message)
        {
            if (obj is null)
            {
                _errorDetails.MessageUser = message;
                _errorDetails.Messages = new List<string> { "The query did not return any results." };
                _errorDetails.StatusCode = HttpStatusCode.NotFound;
                //_errorDetails.CustomErrorGenerate();
            }
        }

        public static void ListIsEmpty<T>(this IEnumerable<T> list, string message)
        {
            if (!list.Any())
            {
                _errorDetails.MessageUser = message;
                _errorDetails.Messages = new List<string> { "The query did not return any results." };
                _errorDetails.StatusCode = HttpStatusCode.NotFound;
                _errorDetails.CustomErrorGenerate();
            }
        }

        public static void ValidEstatusUser(this int userState)
        {
            if (userState != 1)
            {
                string mensaje = "";
                switch (userState)
                {
                    case 2:
                        mensaje = "Usuario inactivo.";
                        break;
                    case 3:
                        mensaje = "Usuario en vacaciones.";
                        break;
                    case 4:
                        mensaje = "Usuario incapacitado.";
                        break;
                    case 5:
                        mensaje = "Usuario de permiso.";
                        break;
                }
                _errorDetails.MessageUser = mensaje;
                _errorDetails.Messages = new List<string> { "The user has a status other than active" };
                _errorDetails.StatusCode = HttpStatusCode.Forbidden;
                _errorDetails.CustomErrorGenerate();
            }
        }

        public static void ValidUserIfIsNull(this UserOLD user)
        {
            if (user is null)
            {
                _errorDetails.MessageUser = "Error de autenticación.";
                _errorDetails.Messages = new List<string> { "Authentication Error" };
                _errorDetails.StatusCode = HttpStatusCode.Forbidden;
                _errorDetails.CustomErrorGenerate();
            }
        }

        public static void ValidSaveChanges(this bool result, string message)
        {
            if (!result)
            {
                _errorDetails.MessageUser = message;
                _errorDetails.Messages = new List<string> { "Error saving changes" };
                _errorDetails.StatusCode = HttpStatusCode.InternalServerError;
                _errorDetails.CustomErrorGenerate();
            }
        }

        public static void ValidTransactionError(this Exception ex, string message)
        {
            _errorDetails.MessageUser = message;
            _errorDetails.Messages = new List<string> { ex.Message };
            _errorDetails.StatusCode = HttpStatusCode.InternalServerError;
            _errorDetails.CustomErrorGenerate();
        }
    }
}
