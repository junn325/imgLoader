using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace imgLoader_WPF
{
    public static class ChangeIP
    {
        public static bool ChangeMACAddress(string mac)
        {
            if (!(mac.Length == 17 && (mac.Length - mac.Replace("-", "").Length) == 5) && mac.Length != 12) return false;

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            var temp = interfaces.Where(i => i.OperationalStatus == OperationalStatus.Up && i.NetworkInterfaceType == NetworkInterfaceType.Ethernet).ToArray();
            if (temp.Length != 1)
            {
                foreach (var net in temp)
                {
                    Core.Log($"ChangeIP: ChangeMacAddress: Muliple Ethernet Adapter: {net.Name}");
                }
            }

            var name = temp[0].Name;
            var desc = temp[0].Description;
            var key = "SYSTEM\\ControlSet001\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}";      //Network Adapter list
            var adaptNum = 0;

            foreach (var i in interfaces)
            {
                adaptNum++;
                if (i.Name == name) break;
            }
            key += $"\\{adaptNum:D4}";

            using var searcher = new ManagementObjectSearcher($"Select * From win32_networkadapter Where Name='{desc}'").Get();
            using var adapter = searcher.Cast<ManagementObject>().First();
            using var reg = Registry.LocalMachine.OpenSubKey(key, true);
            if (reg == null) return false;

            adapter.InvokeMethod("Disable", null);
            reg.SetValue("NetworkAddress", mac);
            adapter.InvokeMethod("Enable", null);

            return true;
        }
    }
}
