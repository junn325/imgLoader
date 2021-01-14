using System;

namespace TestSite
{
    internal static class StrTools
    {
        /// <summary>
        ///  StringSplitOptions.None
        /// </summary>
        public static int StrLen(this string input, char find)
        {
            return input.Split(find).Length - 1;
        }

        /// <summary>
        ///  StringSplitOptions.None
        /// </summary>
        public static int StrLen(this string input, string find)
        {
            return input.Split(find).Length - 1;
        }

        public static string GetStringValue(string source, string name)
        {
            return source.Split($"\"{name}\":\"")[1].Split('\"')[0];
        }

        public static string GetValue(string source, string name, char separator)
        {
            return source.Split($"\"{name}\":{separator}")[1].Split(separator)[0];
        }

        public static string GetValue(string source, string name, char firstSeparator, char lastSeparator)
        {
            return source.Split($"\"{name}\":{firstSeparator}")[1].Split(lastSeparator)[0];
        }

        public static string GetValue(string source, string name)
        {
            return source.Split($"\"{name}\":")[1].Split(',')[0];
        }

        public static string GetBetween(this string source, string firstSeparator, string lastSeparator)
        {
            return source[(source.IndexOf(firstSeparator, StringComparison.Ordinal) + (firstSeparator.Length))..source.LastIndexOf(lastSeparator, StringComparison.Ordinal)];
        }
        public static string GetBetween(this string source, char firstSeparator, char lastSeparator)
        {
            return source[(source.IndexOf(firstSeparator, StringComparison.Ordinal) + 1)..source.LastIndexOf(lastSeparator)];
        }
    }
}