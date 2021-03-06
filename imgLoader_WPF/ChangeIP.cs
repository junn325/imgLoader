using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace imgLoader_WPF
{
    public class ChangeIP
    {
        public bool ChangeMACAddress(string mac)
        {
            if (!(mac.Length == 17 && (mac.Length - mac.Replace("-", "").Length) == 5) && mac.Length != 12) return false;

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            if (interfaces.Length != 0)
            {
                foreach (var net in interfaces)
                {
                    Core.Log($"ChangeIP: ChangeMacAddress: Muliple Ethernet Adapter: {net.Name}");
                }
            }

            var name = interfaces.Where(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet).ToArray()[0].Name;

            var key = "SYSTEM\\ControlSet001\\Control\\Class\\{4d36e972-e325-11ce-bfc1-08002be10318}";      //Network Adapter list
            var adaptNum = 0;

            foreach (var i in interfaces)
            {
                adaptNum++;
                if (i.Name == name) break;
            }
            key += $"\\{adaptNum:D4}";

            var reg = Registry.LocalMachine.OpenSubKey(key, true);
            if (reg == null) return false;

            reg.SetValue("NetworkAddress", mac);

            return true;
        }
    }
}
