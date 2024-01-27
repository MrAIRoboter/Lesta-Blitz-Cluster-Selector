using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lesta_Blitz_Cluster_Selector.Models;
using Lesta_Blitz_Cluster_Selector.Utils;

namespace Lesta_Blitz_Cluster_Selector
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Firewall.EnableFirewall();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}