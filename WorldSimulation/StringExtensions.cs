using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WorldSimulation
{
    public static class StringExtensions
    {
        /// <summary>
        /// Equalses the ignore case.
        /// </summary>
        /// <param name="string1">The string1.</param>
        /// <param name="string2">The string2.</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string string1, string string2)
        {
            return string1.Equals(string2, StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// Replaces at.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="index">The index.</param>
        /// <param name="newChar">The new character.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">input</exception>
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public static bool IsAlliterative(this string input, string comparison = null)
        {
            var s = string.IsNullOrWhiteSpace(comparison) ? input : string.Format("{0} {1}", input, comparison);
            return
                new Regex("[^\\w+]").Split(s)
                    .Where(a => a.Length > 0)
                    .All(a => a.ToLower().StartsWith("" + Char.ToLower(s.First())));
        }
    }
}
