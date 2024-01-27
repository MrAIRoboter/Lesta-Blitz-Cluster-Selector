using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lesta_Blitz_Cluster_Selector.Models;

namespace Lesta_Blitz_Cluster_Selector
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeClustersCheckedListBox();
        }

        private void DeveloperLinkLabel_Click(object sender, EventArgs e) => Process.Start("https://vk.com/mrairobot");

        private void InitializeClustersCheckedListBox()
        {
            if (ClusterSelector.TryInitialize() == false)
            {
                MessageBox.Show("Не удалось загрузить список кластеров! Проверьте-интернет соединение, либо проверьте наличие обновлений программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ClustersCheckedListBox.Items.Clear();

            foreach (KeyValuePair<string, bool> clusterAddressPair in ClusterSelector.ClustersAddresses)
                ClustersCheckedListBox.Items.Add(clusterAddressPair.Key, !clusterAddressPair.Value);

            ClustersCheckedListBox.ItemCheck -= ClustersCheckedListBox_ItemCheck;
            ClustersCheckedListBox.ItemCheck += ClustersCheckedListBox_ItemCheck;

        }

        private void ClustersCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Получаем текст из элемента ClustersCheckedListBox
            string clusterAddress = ClustersCheckedListBox.Items[e.Index].ToString();
            Debug.WriteLine($"ClustersCheckedListBox_ItemCheck ({clusterAddress}) =>");

            // Предполагая, что ваш ClusterSelector.ClustersAddresses представлен в виде Dictionary<string, bool>
            if (ClusterSelector.ClustersAddresses.TryGetValue(clusterAddress, out bool currentValue))
            {
                bool isChecked = (e.NewValue == CheckState.Checked);
                bool isCancelled;

                if (isChecked == true)
                    isCancelled = ClusterSelector.TryUnblockCluster(clusterAddress) == false;
                else
                    isCancelled = ClusterSelector.TryBlockCluster(clusterAddress) == false;

                if (isCancelled == true)
                    e.NewValue = e.CurrentValue;
            }
        }
    }
}
