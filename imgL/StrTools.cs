namespace imgL
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
    }
}