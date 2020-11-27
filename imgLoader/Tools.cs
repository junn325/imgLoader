using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace imgLoader
{
    internal static class ConCtrl
    {
        public static void HorizMid(Control control)
        {
            control.Left = (control.Parent.Width - control.Width) / 2;
        }

        public static void VertMid(Control control)
        {
            control.Top = control.Parent.ClientSize.Height / 2 - control.Height / 2;
        }

    }

    internal static class SysCtrl

    {
        /// <summary>
        /// 이름이 정확히 일치하는 프로세스만 종료
        /// </summary>
        public static void KillAllProcess(string processName)
        {
            foreach (Process item in Process.GetProcessesByName(processName))
            {
                item.Kill();
            }
        }

        /// <summary>
        /// 반환단위: byte
        /// </summary>
        public static long DirSize(DirectoryInfo directory)
        {
            return directory.GetFiles("*", SearchOption.AllDirectories).Sum(item => item.Length);
        }
    }

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
            return input.Split(new string[] { input }, StringSplitOptions.None).Length - 1;
        }

        public static string GetStringValue(string source, string name)
        {
            return source.Split($"\"{name}\":\"")[1].Split('\"')[0];
        }

        public static string GetStringValue(string source, string name, char separator)
        {
            return source.Split($"\"{name}\":{separator}")[1].Split(separator)[0];
        }

        public static string GetStringValue(string source, string name, char firstSeparator, char lastSeparator)
        {
            return source.Split($"\"{name}\":{firstSeparator}")[1].Split(lastSeparator)[0];
        }

        public static string GetValue(string source, string name)
        {
            return source.Split($"\"{name}\":")[1].Split(',')[0];
        }
    }
}