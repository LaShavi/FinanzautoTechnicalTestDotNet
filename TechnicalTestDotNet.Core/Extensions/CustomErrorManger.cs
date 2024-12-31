using TechnicalTestDotNet.Core.Helpers.CustomHttpExceptions;
using TechnicalTestDotNet.Core.Models;

namespace TechnicalTestDotNet.Core.Extensions
{
    public static class CustomErrorManger
    {
        public static void CustomErrorGenerate(this ErrorDetails errorDetails)
        {
            throw new CustomHttpException(errorDetails);
        }

    }
}
