namespace TechnicalTestDotNet.Core.Extensions
{
    public static class TextTransform
    {

        public static string ToTitleCase(this string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string FistLetterToUpper(this string str)
        {
            return str.First().ToString().ToUpper() + str[1..];
        }
    }
}
