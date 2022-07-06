using System.Globalization;
using System.Text;

namespace Pizza.Bll.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Remove accents from string's characters.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The string without accents.</returns>
        public static string RemoveAccents(this string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var chr in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(chr);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(chr);
                }
            }
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Remove more string from a string.
        /// </summary>
        /// <param name="stringToModify">The string to modify.</param>
        /// <param name="arr">The string array containing the elements to be removed.</param>
        /// <returns>The modified string.</returns>
        public static string RemoveStrings(this string stringToModify, string[] arr)
        {
            foreach (var str in arr)
            {
                stringToModify = stringToModify.Replace(str, "");
            }
            return stringToModify;
        }
    }
}
