using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lesta_Blitz_Cluster_Selector.Utils;

namespace Lesta_Blitz_Cluster_Selector.Models
{
    public static class ClusterSelector
    {
        public static Dictionary<string, bool> ClustersAddresses = new Dictionary<string, bool>(); // <address, isBlocked>

        public static bool TryInitialize()
        {
            ClustersAddresses = new Dictionary<string, bool>();

            if(TryUpdateClustersAddresses() == false)
                return false;

            return true;
        }

        public static bool TryBlockCluster(string address)
        {
            if(ClustersAddresses.ContainsKey(address) == false)
                return false;

            if (ClustersAddresses[address] == true)
                return true;

            Firewall.BlockOutTcpConnectionByAddress(address);

            ClustersAddresses[address] = true;

            return true;
        }

        public static bool TryUnblockCluster(string address)
        {
            if (ClustersAddresses.ContainsKey(address) == false)
                return false;

            if (ClustersAddresses[address] == false)
                return true;

            Firewall.UnblockOutTcpConnectionByAddress(address);

            ClustersAddresses[address] = false;

            return true;
        }

        public static bool TryUpdateClustersAddresses()
        {
            List<string> adresses = new List<string>();

            if (Lesta.TryGetClusterAddresses(ref adresses) == false)
                return false;

            ClustersAddresses = new Dictionary<string, bool>();

            foreach (string address in adresses)
                ClustersAddresses.Add(address, Firewall.IsOutTcpConnectionRuleExists(address));

            return true;
        }
    }
}
