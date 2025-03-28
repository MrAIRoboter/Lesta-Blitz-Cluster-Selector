using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesta_Blitz_Cluster_Selector.Utils.WinForms
{
    public static class FormExtensions
    {
        public static void InvokeAction(this Form form, Action action) => form.Invoke(action);
    }
}
