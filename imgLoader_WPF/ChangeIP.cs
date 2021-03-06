using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace imgLoader_WPF
{
    public class ChangeIP
    {
        public void ChangeMacAddress(string mac)
        {
            var ps = PowerShell.Create();
            ps.AddCommand("Get-NetAdapter");
            ps.AddParameter("physical");
            var result = ps.Invoke();

            var name = result[0].Properties["Name"].Value;

            ps.AddScript($"Set-NetAdapter -Name {name} -macAddress \"{mac}\" -Confirm:$false");
            ps.Invoke();
        }
    }
}
