namespace imgL
{
    internal static class StrTools
    {
        public static int StrLen(this string input, char find)
        {
            return input.Split(find).Length - 1;
        }
    }
}