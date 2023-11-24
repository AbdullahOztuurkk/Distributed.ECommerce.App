using System.Text.RegularExpressions;

namespace Clicco.Domain.Core.Extensions
{
    public static class StringExtensions
    {
        public static string AsSlug(this string str)
        {
            str = str.ToLower().Trim();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ");
            str = str.Replace(" ", "-");
            str = Regex.Replace(str, @"-+", "-");
            return str;
        }
    }
}
