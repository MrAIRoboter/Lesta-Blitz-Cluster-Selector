using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetFwTypeLib;

namespace Lesta_Blitz_Cluster_Selector.Utils
{
    public static class Firewall
    {
        public static readonly string BlockOutTcpConnectionByAddressRuleName = "@LBCS Out Tcp Connection Block-";

        public static void BlockOutTcpConnectionByAddress(string address)
        {
            if (IsOutTcpConnectionRuleExists(address) == true)
                return;

            string parameters = "advfirewall firewall add rule " +
                                  "name=\"{0}\" " +
                                  "dir={1} " +
                                  "action={2} " +
                                  "protocol={3} " +
                                  "remoteip=\"{4}\" ";

            string rulename = $"{BlockOutTcpConnectionByAddressRuleName}{address}";
            string direction = "out"; // in,out
            string action = "block"; // allow,block,bypass
            string protocol = "any"; // TCP, UDP
            string remoteIp;

            if (TryGetIPAddressesString(address, out remoteIp, out Exception exception) == false)
                throw exception;

            //string remotePort = addressParts[1]; // "remotePort={5}"

            ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\netsh.exe");
            info.Arguments = String.Format(parameters, rulename, direction, action, protocol, remoteIp);
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            Debug.WriteLine($"Firewall.BlockOutTcpConnectionByAddress().info.Arguments: {info.Arguments}");

            Process.Start(info);
        }

        public static void UnblockOutTcpConnectionByAddress(string address)
        {
            if (IsOutTcpConnectionRuleExists(address) == false)
                return;

            string ruleName = $"{BlockOutTcpConnectionByAddressRuleName}{address}";

            string parameters = "advfirewall firewall delete rule " +
                                  "name=\"{0}\" ";

            ProcessStartInfo info = new ProcessStartInfo(@"C:\Windows\System32\netsh.exe");
            info.Arguments = String.Format(parameters, ruleName);
            info.UseShellExecute = false;
            info.CreateNoWindow = true;

            Debug.WriteLine($"Firewall.UnblockOutTcpConnectionByAddress().info.Arguments: {info.Arguments}");

            Process.Start(info);
        }

        public static bool IsOutTcpConnectionRuleExists(string address)
        {
            string ruleName = $"{BlockOutTcpConnectionByAddressRuleName}{address}";

            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");
            info.Arguments = $"/c chcp 1251 & netsh advfirewall firewall show rule name=\"{ruleName}\"";
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.StandardOutputEncoding = Encoding.GetEncoding(1251);
            info.CreateNoWindow = true;

            Process process = Process.Start(info);
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Debug.WriteLine($"Firewall.IsOutTcpConnectionRuleExists().info.Arguments: {info.Arguments}");
            Debug.WriteLine($"Firewall.IsOutTcpConnectionRuleExists().output.Contains(ruleName): {output.Contains(ruleName)}");

            return output.Contains(ruleName);
        }

        public static bool TryGetIPAddressesString(string domain, out string result, out Exception exception) // result example: 127.0.0.1,192.168.0.1
        {
            result = null;
            exception = null;

            try
            {
                IPAddress[] addresses = Dns.GetHostAddresses(domain);
                result = "";

                for (int i = 0; i < addresses.Length; i++)
                {
                    if (i != 0)
                        result += ",";

                    result += addresses[i].ToString();
                }
            }
            catch(Exception ex)
            {
                exception = ex;
                return false;
            }

            return true;
        }

        public static void EnableFirewall()
        {
            INetFwMgr firewallMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr", false));

            firewallMgr.LocalPolicy.CurrentProfile.FirewallEnabled = true;

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.FirewallEnabled[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PRIVATE] = true;
            firewallPolicy.FirewallEnabled[NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN] = true;
        }
    }
}
